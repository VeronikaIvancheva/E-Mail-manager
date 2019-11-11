using EmailManager.Data;
using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface IEmailService
    {
        Email GetEmail(int emailId);
        Task SaveEmailsToDB();
    }
}
