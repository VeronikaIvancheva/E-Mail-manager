using System.Collections.Generic;
using System.Threading.Tasks;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IUserServices
    {
        User BanUser(string userId);
        IEnumerable<User> GetAll();
        User GetUserById(string id);
        Client GetClientById(int id);
        Task RegisterAccountAsync(User registerAccount);
        User ValidationMethod(User user);
    }
}