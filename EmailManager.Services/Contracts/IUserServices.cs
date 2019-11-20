using System.Threading.Tasks;
using EmailManager.Data.DTO;

namespace EmailManager.Services.Contracts
{
    public interface IUserServices
    {
        Task RegisterAccountAsync(RegisterAccountDTO registerAccountDto);
        RegisterAccountDTO ValidationMethod(RegisterAccountDTO registerAccountDto);
    }
}