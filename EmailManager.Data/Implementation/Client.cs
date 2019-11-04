using EmailManager.Data.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    public class Client : IClient
    {
        [Key]
        public int IdClient { get; set; }
        public string PhoneNumberClient { get; set; }
        public string EGNClient { get; set; }
        public string FirstNameClient { get; set; }
        public string LastNameClient { get; set; }
        public string EmailClient { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
