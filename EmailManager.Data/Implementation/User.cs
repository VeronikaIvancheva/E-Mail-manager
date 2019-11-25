using System;
using System.Collections.Generic;
using EmailManager.Data.Utilities;
using Microsoft.AspNetCore.Identity;

namespace EmailManager.Data.Implementation
{
    public class User : IdentityUser, IAuditableRegistration
    {
        public User()
        {
            this.UserEmails = new HashSet<Email>();
        }

        public User(string name,string role) 
        {
            this.Name = name;
            this.Role = role;
        }

        public string Role { get; set; }
        public string Name { get; set; }
        public ICollection<Email> UserEmails { get; set; }
        public ICollection<Client> Clients { get; set; }

        public DateTime? InitialRegistration { get; set; }
        public DateTime? LastRegistration { get; set; }
        public DateTime? SetCurrentStatus { get; set; }
    }
}
