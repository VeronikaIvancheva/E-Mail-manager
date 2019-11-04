using EmailManager.Data.Implementation;
using System.Collections.Generic;

namespace EmailManager.Data.Contracts
{
    public interface IClient
    {
        string EGNClient { get; set; }
        string EmailClient { get; set; }
        string FirstNameClient { get; set; }
        int IdClient { get; set; }
        string LastNameClient { get; set; }
        ICollection<Loan> Loans { get; set; }
        string PhoneNumberClient { get; set; }
    }
}