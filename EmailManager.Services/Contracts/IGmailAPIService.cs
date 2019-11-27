using System.Threading.Tasks;

namespace EmailManager.Services.Contracts
{
    public interface IGmailAPIService
    {
        Task SaveEmailsToDB();
    }
}