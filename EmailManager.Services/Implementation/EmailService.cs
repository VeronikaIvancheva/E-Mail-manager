using EmailManager.Data;
using EmailManager.Data.Context;
using EmailManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmailManager.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailManagerContext _context;

        public EmailService(EmailManagerContext context)
        {
            this._context = context;
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
