using EmailManager.Data.Context;
using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class CredentialsService : ICredentialsService
    {
        private readonly EmailManagerContext _context;

        public CredentialsService(EmailManagerContext context)
        {
            this._context = context;
        }

        public async Task AddToken(GmailToken token)
        {
            await _context.GmailTokens
                .AddAsync(token);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveToken(GmailToken token)
        {
            _context.GmailTokens
                .Remove(token);

            await _context.SaveChangesAsync();
        }

        public async Task<GmailToken> GetToken()
        {
            var token = await _context.GmailTokens
                .FirstOrDefaultAsync();

            return token;
        }

        public async Task<bool> AccessTokenExist()
        {
            var isExist = await _context.GmailTokens.
                FirstOrDefaultAsync();

            if (isExist != null)
            {
                return true;
            }

            return false;
        }
    }
}
