using EmailManager.Data.Implementation;
using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface ICredentialsService
    {
        Task AddToken(GmailToken token);
        Task RemoveToken(GmailToken token);
        Task<GmailToken> GetToken();
        Task<bool> AccessTokenExist();
    }
}
