using EmailManager.Data;
using EmailManager.Data.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetAllEmails();
        Task<IEnumerable<Email>> GetAllOpenedEmails();
        Task<IEnumerable<Email>> GetAllClosedEmails();
        Task<IEnumerable<Email>> GetAllNewEmails();
        Task<IEnumerable<Email>> GetAllNotReviewedEmails();
        Task<IEnumerable<Email>> GetAllNotValidEmails();

        EmailStatus GetStatus(string emailId);

        Email GetEmail(int emailId);

        Task MarkNewStatus(int loanId, string userId);
        Task MarkClosedStatus(int loanId, string userId);
        Task MarkOpenStatus(int emailId, string userId);
        Task MarkNotReviewStatus(int emailId, string userId);
        Task MarkInvalidStatus(int emailId, string userId);
    }
}
