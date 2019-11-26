using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Models.UserViewModel;
using EmailManager.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmailManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            this._userService = userService;
        }

        public IActionResult Index()
        {
            var allUsers = _userService.GetAll();


            var userListing = allUsers
                .Select(u => new UserViewModel
                {
                    Name = u.Name,
                    Role = u.Role,
                    Email = u.Email,
                    Id = u.Id,
                    InitialRegistration = u.InitialRegistration,
                    UserName = u.UserName,
                    LockOutEnd = u.LockoutEnd,
                    PhoneNumber = u.PhoneNumber,
                    LastRegistration = u.LastRegistration,                    
                })
                .OrderBy(f => f.Id)
                .ToList();

            var userModel = new UserIndexViewModel
            {
                Users = userListing
            };

            return View(userModel);
        }

        public IActionResult Detail(string userId)
        {
            var user = _userService.GetUserById(userId);

            var userModel = new UserViewModel(user);

            return View("Detail", userModel);
        }

        [HttpPost]
        public IActionResult BanUser(string userId)
        {
            var user = _userService.GetUserById(userId);
            _userService.BanUser(userId);

            var userModel = new UserViewModel(user);

            return View("Detail", userModel);
        }
    }
}