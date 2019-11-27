using System.Collections.Generic;
using System.Threading.Tasks;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IUserServices
    {
        User BanUser(string userId);
        IEnumerable<User> GetAll(int currentPage);
        User GetUserById(string id);
        Task<IEnumerable<User>> SearchUsers(string search, int currentPage);
        Task<int> GetPageCount(int emailsPerPage);
    }
}