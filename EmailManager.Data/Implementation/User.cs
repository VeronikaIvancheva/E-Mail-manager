using System.Collections.Generic;
using EmailManager.Data.Contracts;
using Microsoft.AspNetCore.Identity;

namespace EmailManager.Data.Implementation
{
    public class User : IdentityUser, IUser
    {
        public User()
        {
            this.UserEmails = new HashSet<Email>();
        }

        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Email> UserEmails { get; set; }
        public ICollection<Client> Clients { get; set; }

    }
}
