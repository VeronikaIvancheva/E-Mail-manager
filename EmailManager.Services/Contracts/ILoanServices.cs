using System.Threading.Tasks;
using EmailManager.Data.DTO;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface ILoanServices
    {
        Task<bool> ApproveLoanAsync(ApproveLoanDTO approveLoanDto);
        Task<Client> ClientLoanApplication(ClientDTO clientDto);
        ClientDTO ValidationMethod(ClientDTO clientDto);
    }
}