using EmailManager.Data.Implementation;
using System.Collections.Generic;

namespace EmailManager.Data.Contracts
{
    public interface IClient
    {
        string ClientEGN { get; set; }
        string ClientEmail { get; set; }
        int ClientId { get; set; }
        string ClientName { get; set; }
        ICollection<Loan> Loans { get; set; }
        string ClientPhoneNumber { get; set; }
    }
}