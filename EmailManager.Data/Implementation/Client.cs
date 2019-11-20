using EmailManager.Data.Contracts;
using EmailManager.Data.Utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    public class Client : IClient,DataModelHasId
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
        //Not needed for now,but the Client and Loab classes should be reviewed
        //public virtual Email Email { get; set; }
        //public int? EmailId { get; set; }
        
    }
}
