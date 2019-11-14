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
        private readonly UserManager<User> _userManager;

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

        public EmailStatus GetStatus(string emailId)
        {
            var email = _context.Emails
                .FirstOrDefault(b => b.EmailId == emailId);

            return email.EnumStatus;
        }

        public async Task MarkNewStatus(int emailId, string userId)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(a => a.Id == emailId);
            //var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            //await _context.Users
            //    .FirstOrDefaultAsync(c => c.Id == userId);

            //var status = await _context.Status.FirstOrDefaultAsync(s => s.EmailStatus == email.Status.EmailStatus);
            //var stat = status.NewStatus;

            if (email.Status == null)
            {
                email.Status.LastStatus = email.Status.NewStatus;
                email.Status.NewStatus = DateTime.UtcNow;
                email.Status.TimeStamp = DateTime.UtcNow;
                email.Status.ActionTaken = "Changed";
                email.User.Id = userId;
                //user.UserEmails.Add(email);

                email.EnumStatus = EmailStatus.New;
                await _context.SaveChangesAsync();
            }

            //за запазване: enum EmailStatus, DateTime NewStatus, DateTime LastStatus, DateTime TimeStamp (when changed)
            // string ActionTaken
        }

        public async Task MarkClosedStatus(int emailId, string userId)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(a => a.Id == emailId);
            var user = await _context.Users
               .FirstOrDefaultAsync(c => c.Id == userId);

            email.Status.TimeStamp = DateTime.Now;
            email.Status.ActionTaken = "Changed";

            user.UserEmails.Add(email);

            email.EnumStatus = EmailStatus.Closed;

            await _context.SaveChangesAsync();
        }
    }
}
