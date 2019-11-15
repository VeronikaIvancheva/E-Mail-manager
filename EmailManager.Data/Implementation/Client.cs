using EmailManager.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public string ClientFirstName { get; set; }
        public string ClientLastName { get; set; }
        public string ClientEmail { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
