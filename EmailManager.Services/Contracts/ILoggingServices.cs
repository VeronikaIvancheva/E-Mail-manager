using System.Threading.Tasks;
using EmailManager.Data.Context;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface ILoggingServices
    {
        EmailManagerContext _context { get; }

        Task<bool> SaveLastLoginUser(User user);
    }
}