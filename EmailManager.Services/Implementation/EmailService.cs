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

namespace EmailManager.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerContext _context;

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

        public Email GetEmail(int loanId)
        {
            var email = _context.Emails
                .Include(m => m.EmailBody)
                .Include(m => m.Attachments)
                .Include(m => m.Status)
                .FirstOrDefault(m => m.LoanId == loanId);

            return email;
        }

        public EmailStatus GetStatus(string emailId)
        {
            var email = _context.Emails
                .FirstOrDefault(b => b.EmailId == emailId);

            return email.EnumStatus;                
        }

        public async Task MarkNewStatus(int loanId, string userId)
        {
            var email = await _context.Emails
                .FirstOrDefaultAsync(a => a.LoanId == loanId);
            var status = await _context.Status
                .FirstOrDefaultAsync(a => a.StatusID == email.Status.StatusID);
            var user = await _context.User
                .FirstOrDefaultAsync(c => c.Id == userId);

            email.Status.NewStatus = DateTime.Now;

            user.UserEmails.Add(email);

            email.EnumStatus = EmailStatus.New;
            //да сложим другите datetime и ест, които ги има в таблицата на Status

            await _context.SaveChangesAsync();
        }
    }
}
