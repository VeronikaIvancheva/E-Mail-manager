using EmailManager.Data;
using EmailManager.Data.Context;
using EmailManager.Data.Enums;
using EmailManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Data.Implementation;
using Microsoft.Extensions.Logging;
using EmailManager.Data.DTO;
using EmailManager.Services.Exeptions;

namespace EmailManager.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerContext _context;
        private readonly ILogger _logger;
        private readonly IEncryptionServices _securityEncrypt;
        private readonly IDecryptionServices _securityDecrypt;

        //private readonly UserManager<User> _userManager;
        public EmailService(EmailManagerContext context, ILogger<EmailService> logger,
            IEncryptionServices securityEncrypt,IDecryptionServices securityDecrypt)
        {
            this._context = context;
            this._logger = logger;
            this._securityEncrypt = securityEncrypt;
            this._securityDecrypt = securityDecrypt;
            this._securityEncrypt = securityEncrypt;
        }

        public async Task<IEnumerable<Email>> GetAllEmails()
        {
            IEnumerable<Email> emailAll = await _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .Include(m => m.User)
                .OrderByDescending(m => m.Id)
                .ToListAsync();

            _logger.LogInformation("System listing all emails.");
            
            return emailAll;
        }

        public async Task<IEnumerable<Email>> GetAllStatusEmails(string statusEmail)
        {
            EmailStatus status;

            if (statusEmail == "New")
            {
                status = EmailStatus.New;
                _logger.LogInformation("System listing all emails - status New.");
            }
            else if(statusEmail == "Approved")
            {
                status = EmailStatus.Approved;
                _logger.LogInformation("System listing all emails - status Approved.");
            }
            else if (statusEmail == "Rejected")
            {
                status = EmailStatus.Rejected;
                _logger.LogInformation("System listing all emails - status Rejected.");
            }
            else if(statusEmail == "NotReviewed")
            {
                status = EmailStatus.NotReviewed;
                _logger.LogInformation("System listing all emails - status NotReviewed.");
            }
            else if(statusEmail == "NotValid")
            {
                status = EmailStatus.NotValid;
                _logger.LogInformation("System listing all emails - status NotValid.");
            }
            else if(statusEmail == "Open")
            {
                status = EmailStatus.Open;
                _logger.LogInformation("System listing all emails - status Open.");
            }
            else
            {
                _logger.LogInformation("System failed to get list of emails statuses.");
                throw new Exception("There are no status like this in the enums.");
            }

            _logger.LogInformation($"System listing all emails - status {statusEmail}.");

            IEnumerable<Email> emailAllStatus = await _context.Emails
                .Where(s => s.EnumStatus == (status))
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .Include(m => m.User)
                .OrderByDescending(m => m.Status.NewStatus)
                .ToListAsync();

            return emailAllStatus;
        }

        public Email GetEmail(int emailId)
        {
            Email email = _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .Include(m => m.User)
                .FirstOrDefault(m => m.Id == emailId);

            if (email == null)
            {
                _logger.LogWarning($"System didn't find email with id: {emailId}");
            }

            _logger.LogWarning($"User look for email with id: {emailId}");

            return email;
        }
        
        public Attachment GetAttachment(int emailId)
        {
            Email email = _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .Include(m => m.User)
                .FirstOrDefault(m => m.Id == emailId);

            var attachment = _context.Attachments
                .Include(a => a.Email)
                .FirstOrDefault(e => email.Id == emailId);

            _logger.LogInformation($"Server fetch attachment for email with Id: {emailId}");

            return attachment;
        }

        public EmailStatus GetStatus(string emailId)
        {
            Email email = _context.Emails
                .FirstOrDefault(b => b.EmailId == emailId);

            _logger.LogInformation($"Email status sent. Email id: {emailId}");

            return email.EnumStatus;
        }

        public async Task MarkNewStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.New;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to new. Email Id: {emailId}");
        }

        public async Task MarkClosedApprovedStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.Approved;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to closed - approved. Email Id: {emailId} by user id: {userId}.");
        }

        public async Task MarkClosedRejectedStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.Rejected;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to closed - rejected. Email Id: {emailId} by user id: {userId}.");
        }

        public async Task MarkOpenStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.Open;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to open. Email Id: {emailId}");
        }

        public async Task MarkNotReviewStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.NotReviewed;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to not review. Email Id: {emailId}");
        }

        public async Task MarkInvalidStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.NotValid;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to invalid. Email Id: {emailId}");
        }

        public async Task<Email> EmailRepeatedPart(int emailId, string userId)
        {
            Email email = await _context.Emails
                .Include(a => a.Status)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == emailId);

            var user = _context.Users.FirstOrDefault(x => x.Id == userId);

            var status = new Status
            {
                LastStatus = email.Status.LastStatus = email.Status.NewStatus,
                NewStatus = email.Status.NewStatus = DateTime.UtcNow,
                TimeStamp = email.Status.TimeStamp = DateTime.UtcNow,
                ActionTaken = email.Status.ActionTaken = "Changed",                
            };
            

            email.User = user;
            user.UserEmails.Add(email);

            _logger.LogInformation($"Changed general email statuses. Email Id: {emailId}");

            return email;
        }

        public async Task AddAttachmentAsync(EmailAttachmentDTO attachmentDTO)
        {
            if (attachmentDTO.FileName.Length < 1 || attachmentDTO.FileName.Length > 100)
            {
                throw new EmailExeptions("Lenght of attachment name is not correct!");
            }

            var gmaiId = await this._context.Emails
               .FirstOrDefaultAsync(id => id.EmailId == attachmentDTO.EmailId);

            if (gmaiId == null)
            {
                var attachment = new Attachment
                {
                    FileName = attachmentDTO.FileName,
                    AttachmentSizeKb = attachmentDTO.AttachmentSizeKb,
                    EmailId = attachmentDTO.EmailId
                };

                await this._context.Attachments.AddAsync(attachment);
                await this._context.SaveChangesAsync();
            }
        }
        public async Task<Email> AddBodyToCurrentEmailAsync(EmailBodyDTO emailBodyDto)
        {
            var email = await this._context.Emails
                .Include(u => u.User)
                .Where(gMail => gMail.EmailId == emailBodyDto.UserId)
                .SingleOrDefaultAsync();

            var emailBody = await this._context.EmailBodies
                .Include(b => b.Email)
                .Where(b => b.EmailId == emailBodyDto.EmailId)
                .SingleOrDefaultAsync();
                
            var currentUser = await this._context.Users
                .Where(id => id.Id == emailBodyDto.UserId)
                .SingleOrDefaultAsync();

            if (emailBodyDto.Body == null)
            {
                throw new EmailExeptions($"The Email with Id {emailBodyDto.Email} does not exist");
            }

            if (emailBodyDto.Body.Length > 1000)
            {
                throw new EmailExeptions($"Body of email is to long!");
            }

            if (emailBody != null)
            {
                throw new EmailExeptions($"Email with the following id {emailBodyDto.Email} contains body");
            }

            var decodeBody = this._securityDecrypt.Base64Decrypt(emailBodyDto.Body);

            var encriptBody = this._securityEncrypt.Encrypt(decodeBody);


            if (email.EmailBody == null)
            {
                emailBody.Body = encriptBody;
                email.User = currentUser;
                email.UserId = emailBodyDto.UserId;
                email.IsSeen = true;
                await this._context.SaveChangesAsync();
            }
            return email;
        }
    }
}
