using System.Threading.Tasks;
using EmailManager.Data;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface ILoanServices
    {
        //Task<bool> ApproveLoan(ApproveLoan approveLoan);
        Task<Loan> CreateLoanApplication(Client client, int loanSum, string userId, int emailId);
        bool CheckEgnValidity(string email);
        Client EncryptClientInfo(string clientName, string clientPhone, string clientEGN, string clientEmail, string userId);
        Client DecryptClientInfo(Client clientId);
        Task<Client> AddClient(string clientName, string clientPhone, string clientEGN, string clientEmail, string userId);
    }
}