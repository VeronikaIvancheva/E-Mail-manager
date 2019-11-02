﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EmailManager.Data
{
    public class User : IdentityUser
    {
        //Мисля Role Enum както каза Мадин
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Email> UserEmails { get; set; }
    }
}
