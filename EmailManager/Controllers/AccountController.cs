using EmailManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _user;

        public AccountController(IUserServices user)
        {
            this._user = user;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

     

    }
}
