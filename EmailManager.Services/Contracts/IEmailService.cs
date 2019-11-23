using EmailManager.Data;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetAllStatusEmails(string statusEmail, int currentPage);

        EmailStatus GetStatus(string emailId);

        Email GetEmail(int emailId);
        Attachment GetAttachment(int emailId);

        Task MarkNewStatus(int loanId, string userId);
        Task MarkClosedApprovedStatus(int emailId, string userId);
        Task MarkClosedRejectedStatus(int emailId, string userId);
        Task MarkOpenStatus(int emailId, string userId);
        Task MarkNotReviewStatus(int emailId, string userId);
        Task MarkInvalidStatus(int emailId, string userId);

        Task<IEnumerable<Email>> SearchEmails(string search, int currentPage);
        Task<int> GetPageCount(int barsPerPage);
    }
}
