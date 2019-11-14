using EmailManager.Data;
using EmailManager.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetAllEmails();
        EmailStatus GetStatus(string emailId);
        Email GetEmail(int emailId);
        Task MarkNewStatus(int loanId, string userId);
        Task MarkClosedStatus(int loanId, string userId);
    }
}
