using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data
{
    public class User: IdentityUser
    {
        //Мисля Role Enum както каза Мадин
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //тук не ми е 100% ясно защо иска override
        public override string Email { get; set; }
    }
}
