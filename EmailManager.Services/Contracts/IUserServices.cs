using System.Threading.Tasks;
using EmailManager.Services.DTO;

namespace EmailManager.Services.Contracts
{
    public interface IUserServices
    {
        Task RegisterAccountAsync(RegisterAccountDTO registerAccountDto);
        RegisterAccountDTO ValidationMethod(RegisterAccountDTO registerAccountDto);
    }
}