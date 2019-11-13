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
    }
}
