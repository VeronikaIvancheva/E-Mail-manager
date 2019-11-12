using System.Collections.Generic;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;

namespace EmailManager.Data.Contracts
{
    public interface IEmail
    {
        ICollection<Attachment> Attachments { get; set; }
        int EmailId { get; set; }
        bool IsValid { get; set; }
        Loan Loan { get; set; }
        int LoanId { get; set; }
        string Sender { get; set; }
        EmailStatus Status { get; set; }
        string Subject { get; set; }
        User User { get; set; }
    }
}