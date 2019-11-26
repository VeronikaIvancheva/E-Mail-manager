using EmailManager.Data.Context;
using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using EmailManager.Services.Exeptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EmailManagerContext _context;
        private readonly UserManager<User> _userManager;

        public UserServices(EmailManagerContext context, UserManager<User> userManager)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task RegisterAccountAsync(User registerAccount)
        {
            var validationMethod = ValidationMethod(registerAccount);

            var user = await this._context.Users
                .Where(name => name.UserName == validationMethod.UserName)
                .Select(username => username.UserName)
                .SingleOrDefaultAsync();

            if (user != null)
            {
                throw new UserExeptions($"You cannot register accout with the following username {validationMethod.UserName}");
            }
        }

        public User ValidationMethod(User user)
        {
            if (user.Role != "Manager" && user.Role != "Operator")
            {
                throw new UserExeptions("Wrong role name!");
            }

            if (user.UserName.Length < 3 || user.UserName.Length > 50)
            {
                throw new UserExeptions("Username must be betweeen 3 and 50 symbols!");
            }

            if (user.Email == null)
            {
                throw new UserExeptions("Email cannot be null!");
            }

            if (user.Email.Length < 5 || user.Email.Length > 50)
            {
                throw new UserExeptions("Email must be betweeen 5 and 50 symbols!");
            }
            return user;
        }

        public User GetUserById(string id)
        {
            return _context.User
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<User> GetAll(int currentPage)
        {
            IEnumerable<User> userAll;

            if (currentPage == 1)
            {
                userAll = _context.Users
                     .OrderBy(u => u.Id)
                     .Take(10)
                     .ToList();
            }
            else
            {
                userAll = _context.Users
                    .OrderBy(u => u.Id)
                    .Skip((currentPage - 1) * 10)
                    .Take(10)
                    .ToList();
            }

            log.Info("System listing all users.");

            return userAll;
        }

        public User BanUser(string userId)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == userId);

            user.LockoutEnabled = true;
            var bannedTill = user.LockoutEnd = DateTime.Now.AddDays(30);

            _context.SaveChanges();
            log.Info($"User with id {userId} has been banned till {bannedTill}");

            return user;
        }

        public async Task<IEnumerable<User>> SearchUsers(string search, int currentPage)
        {
            IEnumerable<User> searchResult = _context.Users
                .Where(b => b.Name.Contains(search) ||
                       b.UserName.Contains(search) ||
                       b.Email.Contains(search) ||
                       b.Id.Contains(search) ||
                       b.Role.ToLower().Contains(search.ToLower())
                       )
                .OrderBy(b => b.Role)
                .ThenBy(b => b.Id);

            if (currentPage == 1)
            {
                searchResult = searchResult
                    .Take(10)
                    .ToList();
            }
            else
            {
                searchResult = searchResult
                   .Skip((currentPage - 1) * 10)
                   .Take(10)
                   .ToList();
            }

            log.Info($"User searched for: {search}");

            return searchResult;
        }

        public async Task<int> GetPageCount(int emailsPerPage)
        {
            var allEmails = await _context.Emails
                .CountAsync();

            var totalPages = (allEmails - 1) / emailsPerPage + 1;

            return totalPages;
        }
    }
}
