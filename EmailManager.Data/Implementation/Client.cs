using EmailManager.Data.Contracts;
using EmailManager.Data.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailManager.Data.Implementation
{
    public class Client : IClient
    {
        public Client()
        {
            this.Loans = new HashSet<Loan>();
        }

        [Key]
        public int ClientId { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEGN { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public ICollection<Loan> Loans { get; set; }
        public bool IsApproved { get; set; }
        //Testing
        public User User { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public virtual Email Email { get; set; }
        [NotMapped]
        public string EmailId { get; set; }

    }
}
