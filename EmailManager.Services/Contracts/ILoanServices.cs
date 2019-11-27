using System.Threading.Tasks;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface ILoanServices
    {
        Task<Loan> CreateLoanApplication(Client client, int loanSum, string userId, int emailId);
        Task<Client> AddClient(string clientName, string clientPhone, string clientEGN, string clientEmail, string userId);
    }
}