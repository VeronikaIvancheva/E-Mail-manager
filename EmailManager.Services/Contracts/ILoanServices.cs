using System.Threading.Tasks;
using EmailManager.Data.DTO;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface ILoanServices
    {
        Task<bool> ApproveLoan(ApproveLoanDTO approveLoanDto);
        Task<Client> CreateLoanApplication(ClientDTO clientDto, int clientId, string userId);
        bool CheckEgnValidity(string email);
        Client EncryptClientInfo(ClientDTO clientId);
        Client DecryptClientInfo(ClientDTO clientId);
    }
}