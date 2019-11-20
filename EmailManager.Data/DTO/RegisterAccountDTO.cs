using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.DTO
{
    public class RegisterAccountDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string CurrentUserId { get; set; }
    }
}
