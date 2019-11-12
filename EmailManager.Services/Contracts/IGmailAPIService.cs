using System.Threading.Tasks;
using EmailManager.Data;

namespace EmailManager.Services.Contracts
{
    public interface IGmailAPIService
    {
        Email GetEmail(string emailId);
        Task SaveEmailsToDB();
    }
}