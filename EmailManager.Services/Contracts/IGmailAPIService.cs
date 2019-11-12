using System.Threading.Tasks;
using EmailManager.Data;

namespace EmailManager.Services.Contracts
{
    public interface IGmailAPIService
    {
        Email GetEmail(int emailId);
        string GetRefreshToken(string gmailId);
        Task SaveEmailsToDB();
        bool SaveRefreshToken(string gmailId, string refreshToken);
        bool UpdateRefreshToken(string gmailId, string refreshToken);
    }
}