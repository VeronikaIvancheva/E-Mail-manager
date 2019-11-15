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

namespace EmailManager.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerContext _context;
        //private readonly UserManager<User> _userManager;

        public EmailService(EmailManagerContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Email>> GetAllEmails()
        {
            return await _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .OrderBy(m => m.EmailId)
                .ToListAsync();
        }

        public Email GetEmail(int mailId)
        {
            var email = _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .FirstOrDefault(m => m.Id == mailId);

            return email;
        }


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

        public EmailStatus GetStatus(string emailId)
        {
            var email = _context.Emails
                .FirstOrDefault(b => b.EmailId == emailId);

            return email.EnumStatus;
        }

        public async Task MarkNewStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.New;
            await _context.SaveChangesAsync();
        }

        public async Task MarkClosedStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.Closed;
            await _context.SaveChangesAsync();
        }

        public async Task MarkOpenStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.Open;
            await _context.SaveChangesAsync();
        }

        public async Task MarkNotReviewStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.NotReviewed;
            await _context.SaveChangesAsync();
        }

        public async Task MarkInvalidStatus(int emailId, string userId)
        {
            var email = EmailRepeatedPart(emailId, userId);

            email.Result.EnumStatus = EmailStatus.NotValid;
            await _context.SaveChangesAsync();
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

            return email;
        }
    }
}
