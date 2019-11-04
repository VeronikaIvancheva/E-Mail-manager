using EmailManager.Data;

namespace EmailManager.Services.Contracts
{
    public interface IEmailService
    {
        Email GetEmail(int emailId);
    }
}
