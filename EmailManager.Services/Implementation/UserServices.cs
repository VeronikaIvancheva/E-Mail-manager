using EmailManager.Data.Context;
using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private static readonly log4net.ILog log =
           log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EmailManagerContext _context;

        public UserServices(EmailManagerContext context, UserManager<User> userManager)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User GetUserById(string id)
        {
            return _context.User
                .FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<User> GetAll(int currentPage)
        {
            IEnumerable<User> userAll = _context.Users
                     .OrderBy(u => u.Id);

            if (currentPage == 1)
            {
                userAll = userAll
                     .Take(10)
                     .ToList();
            }
            else
            {
                userAll = userAll
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
