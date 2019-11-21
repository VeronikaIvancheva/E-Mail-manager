using EmailManager.Data;
using EmailManager.Data.Context;
using EmailManager.Data.Enums;
using EmailManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using EmailManager.Data.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using EmailManager.Data.DTO;
using EmailManager.Services.Exeptions;

namespace EmailManager.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerContext _context;
        private readonly ILogger _logger;
        private readonly UserManager<User> _userManager;
        private readonly IEncryptionAndDecryptionServices _security;
        public EmailService(EmailManagerContext context, ILogger<EmailService> logger,
            IEncryptionAndDecryptionServices security)
        {
            this._context = context;
            this._logger = logger;
            this._security = security;
        }

        public async Task<IEnumerable<Email>> GetAllEmails()
        {
            _logger.LogInformation("System listing all emails.");

            IEnumerable<Email> emailAll = await _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderBy(m => m.Id)
                .ToListAsync();

            return emailAll;
        }

        public async Task<IEnumerable<Email>> GetAllOpenedEmails(string statusEmail)
        {
            _logger.LogInformation("System listing all emails - status Open.");

            IEnumerable<Email> emailAllOpen = await _context.Emails
                .Where(s => s.EnumStatus == (EmailStatus.New))
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderByDescending(m => m.ReceiveDate)
                .ToListAsync();

            return emailAllOpen;
        }

        public async Task<IEnumerable<Email>> GetAllClosedEmails(string statusEmail)
        {
            _logger.LogInformation("System listing all emails - status Close.");

            IEnumerable<Email> emailAllClosed = await _context.Emails
                .Where(s => s.EnumStatus == (EmailStatus.Closed))
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderByDescending(m => m.ReceiveDate)
                .ToListAsync();

            return emailAllClosed;
        }

        public async Task<IEnumerable<Email>> GetAllNewEmails(string statusEmail)
        {
            _logger.LogInformation("System listing all emails - status New.");

            IEnumerable<Email> emailAllNew = await _context.Emails
                .Where(s => s.EnumStatus == (EmailStatus.New))
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderByDescending(m => m.ReceiveDate)
                .ToListAsync();

            return emailAllNew;
        }

        public async Task<IEnumerable<Email>> GetAllNotReviewedEmails(string statusEmail)
        {
            _logger.LogInformation("System listing all emails - status Not Reviewed.");

            IEnumerable<Email> emailAllNotReviewed = await _context.Emails
                .Where(s => s.EnumStatus == (EmailStatus.NotReviewed))
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderByDescending(m => m.ReceiveDate)
                .ToListAsync();

            return emailAllNotReviewed;
        }

        public async Task<IEnumerable<Email>> GetAllNotValidEmails(string statusEmail)
        {
            _logger.LogInformation("System listing all emails - status Not Valid.");

            IEnumerable<Email> emailAllNotValid = await _context.Emails
                .Where(s => s.EnumStatus == (EmailStatus.NotValid))
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderByDescending(m => m.ReceiveDate)
                .ToListAsync();

            return emailAllNotValid;
        }

        public Email GetEmail(int mailId)
        {
            var email = _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .FirstOrDefault(m => m.Id == mailId);

            if (email == null)
            {
                _logger.LogWarning($"System didn't find email with id: {mailId}");
            }

            _logger.LogWarning($"User look for email with id: {mailId}");

            return email;
        }

        #region
        //TODO - to make new status every time and to save it to the db
        //public async Task MakeNewStatus(Email email)
        //{
        //    var lastStatus = email.Status.NewStatus;

        //    Status status = new Status
        //    {
        //        LastStatus = lastStatus,
        //        ActionTaken = "Changed",
        //        TimeStamp = DateTime.UtcNow,
        //        NewStatus = DateTime.UtcNow
        //    };

        //    await _context.Statuses.AddAsync(status);
        //    await _context.SaveChangesAsync();
        //}
        #endregion

        public EmailStatus GetStatus(string emailId)
        {
            var email = _context.Emails
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

        public async Task MarkClosedStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.Closed;
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Changed email status to closed. Email Id: {emailId}");
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

            email.Status.LastStatus = email.Status.NewStatus;
            email.Status.NewStatus = DateTime.UtcNow;
            email.Status.TimeStamp = DateTime.UtcNow;
            email.Status.ActionTaken = "Changed";
            email.User = user;
            user.UserEmails.Add(email);

            _logger.LogInformation($"Changed general email statuses. Email Id: {emailId}");

            return email;
        }

        public async Task AddAttachmentAsync(EmailAttachmentDTO attachmentDTO)
        {
            if (attachmentDTO.FileName.Length < 5 || attachmentDTO.FileName.Length > 100)
            {
                throw new EmailExeptions("Lenght of attachment name is not correct!");
            }

            if (attachmentDTO.EmailId.Length < 5 || attachmentDTO.EmailId.Length > 100)
            {
                throw new EmailExeptions("Lenght of EmailId is not correct!");
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

            var decodeBody = this._security.Base64Decrypt(emailBodyDto.Body);

            var encriptBody = this._security.Encrypt(decodeBody);

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
