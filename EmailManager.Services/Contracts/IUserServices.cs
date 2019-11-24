using EmailManager.Data.Implementation;
using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface IUserServices
    {
        Task RegisterAccountAsync(User user);
        User ValidationMethod(User user);
    }
}