using EmailManager.Data;
using EmailManager.Data.Context;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class GmailAPIService : IGmailAPIService
    {
        private readonly EmailManagerContext _context;
        private readonly ILogger _logger;

        public GmailAPIService(EmailManagerContext context, ILogger<GmailAPIService> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task SaveEmailsToDB()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/gmail-dotnet-quickstart.json
            string[] Scopes = { GmailService.Scope.GmailReadonly };
            string ApplicationName = "Gmail API .NET Quickstart";

            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";

                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));

                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
                
            });

            var emailListRequest = service.Users.Messages.List("emailmanager13@gmail.com");

            emailListRequest.LabelIds = "INBOX";
            emailListRequest.IncludeSpamTrash = false;

            var emailListResponse = emailListRequest.ExecuteAsync().Result;

            if (emailListResponse != null && emailListResponse.Messages != null)
            {
                foreach (var email in emailListResponse.Messages)
                {
                    var emailRequest = service.Users.Messages.Get("emailmanager13@gmail.com", email.Id);

                    //Collection with full email response
                    Message emailFullResponse = emailRequest.ExecuteAsync().Result;


                    //Getting email Subject, From and Date
                    string subject = emailFullResponse.Payload.Headers
                            .FirstOrDefault(s => s.Name == "Subject").Value;

                    string sender = emailFullResponse.Payload.Headers
                        .FirstOrDefault(s => s.Name == "From").Value;

                    string date = emailFullResponse.Payload.Headers
                        .FirstOrDefault(d => d.Name == "Date").Value;

                    string editedDate = date.Remove(date.IndexOf('+') - 1);

                    //Checking whether the emails are saved or not 
                    //TODO - може да се счупи, когато започнем да криптираме клиента
                    Email emailCheck = _context.Emails
                        .FirstOrDefault(e => e.Sender == sender && e.Subject == subject && e.ReceiveDate == editedDate);

                    if (emailCheck == null)
                    {
                        _logger.LogInformation("Start creating new emails.");
                        PassEmailParams(emailFullResponse, editedDate, subject, sender);
                    }
                }
            }
        }

        public EmailBody PassEmailBodyParams(Message emailFullResponse)
        {
            EmailBody emailBody = new EmailBody
            {
                Body = emailFullResponse.Snippet,
            };

            _context.EmailBodies.AddAsync(emailBody);
            _context.SaveChangesAsync();

            return emailBody;
        }

        public Status PassStatusParams()
        {
            Status status = new Status
            {
                ActionTaken = "Changed",
                NewStatus = DateTime.UtcNow,
                LastStatus = DateTime.UtcNow,
                EmailStatus = EmailStatus.NotReviewed,
            };

            _context.Statuses.AddAsync(status);
            _context.SaveChangesAsync();

            return status;
        }

        public bool PassAttachmentParams(Message emailFullResponse)
        {
            var parts = emailFullResponse.Payload.Parts;

            foreach (var item in parts)
            {
                if (item.Body.AttachmentId != null)
                {
                    Attachment attachmentParts = new Attachment
                    {
                        AttachmentId = item.Body.AttachmentId,
                        AttachmentSizeKb = item.Body.Size,
                        FileName = item.Filename,
                        EmailId = emailFullResponse.Id,
                    };

                    _context.Attachments.AddAsync(attachmentParts);
                    _context.SaveChangesAsync();

                    return true;
                }
            }            

            return false;
        }

        public void PassEmailParams(Message emailFullResponse, string editedDate, string subject, string sender)
        {
            var body = PassEmailBodyParams(emailFullResponse);
            var status = PassStatusParams();
            var attachmentBool = PassAttachmentParams(emailFullResponse);

            Email emailParts = new Email
            {
                EmailId = emailFullResponse.Id,
                HasAttachments = attachmentBool,
                EmailBody = body,
                SetCurrentStatus = DateTime.UtcNow,
                ReceiveDate = editedDate,
                Subject = subject,
                Sender = sender,
                Status = status,
                UserId = "NoUser",
            };

            _context.AddAsync(emailParts);
            _context.SaveChangesAsync();
        }
    }
}
