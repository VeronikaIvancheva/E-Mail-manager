using EmailManager.Data;
using EmailManager.Data.Context;
using EmailManager.Services.Contracts;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerContext _context;
        static string[] Scopes = { GmailService.Scope.GmailModify };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static string EmailAddress = "emailmanager13@gmail.com";

        public EmailService(EmailManagerContext context)
        {
            this._context = context;
        }

        public async Task SaveEmailsToDB()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/gmail-dotnet-quickstart.json

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

                    //Collection with full email response.
                    var emailFullResponse = emailRequest.ExecuteAsync().Result;
                }
            }

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("emailmanager13@gmail.com");

            // List labels.
            /*IList<Label>*/
            var labels = request.Execute().Labels;

            Console.WriteLine("Labels:");

            if (labels != null && labels.Count > 0)
            {
                foreach (var labelItem in labels)
                {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            }
            else
            {
                Console.WriteLine("No labels found.");
            }


            //Къде да сефйна, за да ги запазва и DB?
            await _context.SaveChangesAsync();
        }

        public Email GetEmail(int emailId)
        {
            var email = _context.Emails
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .FirstOrDefault(m => m.EmailId == emailId);

            if (email == null)
            {
                throw new ArgumentException();
            }

            return email;
        }
    }
}
