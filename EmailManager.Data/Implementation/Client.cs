using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmailManager.Data.Implementation
{
    public class Client 
    {
        public Client()
        {
            this.Loans = new HashSet<Loan>();
        }

        public Client(string clientTel, string clientEGN, string clientName, string clientEmail, int loanSum)
        {
            this.ClientEGN = clientEGN;
            this.ClientEmail = clientEmail;
            this.ClientName = clientName;
            this.ClientPhoneNumber = clientTel;
            this.LoanedSum = loanSum;
        }

        [Key]
        public int ClientId { get; set; }
        public string ClientPhoneNumber { get; set; }
        public string ClientEGN { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public User User { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public virtual Email Email { get; set; }
        [NotMapped]
        public string EmailId { get; set; }
        [NotMapped]
        public int LoanedSum { get; set; }
    }
}
