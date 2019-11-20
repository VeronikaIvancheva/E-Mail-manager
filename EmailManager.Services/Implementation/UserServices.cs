using EmailManager.Data.Context;
using EmailManager.Data.DTO;
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
        private readonly ILogger<UserServices> _logger;
        private readonly EmailManagerContext _context;
        private readonly UserManager<User> _userManager;

        public UserServices(EmailManagerContext context, UserManager<User> userManager, ILogger<UserServices> logger)
        {
            this._context = context;
            this._userManager = userManager;
            this._logger = logger;
        }

        public async Task RegisterAccountAsync(RegisterAccountDTO registerAccountDto)
        {
            var validationMethod = ValidationMethod(registerAccountDto);

            var user = await this._context.Users
                .Where(name => name.UserName == validationMethod.UserName)
                .Select(username => username.UserName)
                .SingleOrDefaultAsync();

            if (user != null)
            {
                throw new UserExeptions($"You cannot register accout with the following username {validationMethod.UserName}");
            }
        }

        public RegisterAccountDTO ValidationMethod(RegisterAccountDTO registerAccountDto)
        {
            if (registerAccountDto.Role != "Manager" && registerAccountDto.Role != "Operator")
            {
                throw new UserExeptions("Wrong role name!");
            }

            if (registerAccountDto.UserName.Length < 3 || registerAccountDto.UserName.Length > 50)
            {
                throw new UserExeptions("Username must be betweeen 3 and 50 symbols!");
            }

            if (registerAccountDto.Password.Length < 5 || registerAccountDto.Password.Length > 100)
            {
                throw new UserExeptions("Password must be betweeen 5 and 50 symbols!");
            }

            if (registerAccountDto.Email == null)
            {
                throw new UserExeptions("Email cannot be null!");
            }

            if (registerAccountDto.Email.Length < 5 || registerAccountDto.Email.Length > 50)
            {
                throw new UserExeptions("Email must be betweeen 5 and 50 symbols!");
            }
            return registerAccountDto;
        }
    }
}
