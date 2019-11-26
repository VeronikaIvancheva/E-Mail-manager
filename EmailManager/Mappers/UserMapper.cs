using EmailManager.Data.Implementation;
using EmailManager.Models.UserViewModel;
using EmailManager.Services.Contracts;
using System.Collections.Generic;

namespace EmailManager.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel MapFromUser(this User user, IUserServices userService)
        {
            var emailListing = new UserViewModel
            {
                Name = user.Name,
                Role = user.Role,
                Email = user.Email,
                Id = user.Id,
                InitialRegistration = user.InitialRegistration,
                UserName = user.UserName,
                LockOutEnd = user.LockoutEnd,
                PhoneNumber = user.PhoneNumber,
                LastRegistration = user.LastRegistration,
            };

            return emailListing;
        }

        public static UserIndexViewModel MapFromUserIndex(this IEnumerable<UserViewModel> user, int currentPage, int totalPages)
        {
            var model = new UserIndexViewModel
            {
                CurrentPage = currentPage,
                TotalPages = totalPages,
                Users = user
            };

            return model;
        }
    }
}
