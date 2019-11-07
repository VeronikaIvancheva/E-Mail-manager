using EmailManager.Data.Implementation;
using System.Collections.Generic;

namespace EmailManager.Data.Contracts
{
    public interface IClient
    {
        string ClientEGN { get; set; }
        string ClientEmail { get; set; }
        string ClientFirstName { get; set; }
        int ClientId { get; set; }
        string ClientLastName { get; set; }
        ICollection<Loan> Loans { get; set; }
        string ClientPhoneNumber { get; set; }
    }
}