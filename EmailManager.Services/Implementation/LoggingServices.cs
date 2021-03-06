﻿using EmailManager.Data.Context;
using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class LoggingServices : ILoggingServices
    {
        public LoggingServices(EmailManagerContext context)
        {
            _context = context;
        }

        public EmailManagerContext _context { get; }

        public async Task<bool> SaveLastLoginUser(User user)
        {
            if (user == null)
            {
                return false;
            }

            //TO CHECK
          //  user.LastRegistration = DateTime.Now;
            await this._context.SaveChangesAsync();

            return true;
        }

    }
}
