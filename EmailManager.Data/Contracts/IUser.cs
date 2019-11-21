using EmailManager.Data.Implementation;
using System.Collections.Generic;

namespace EmailManager.Data.Contracts
{
    public interface IUser
    {
        string Name { get; set; }
        string Role { get; set; }
        ICollection<Email> UserEmails { get; set; }
    }
}