using EmailManager.Data.Implementation;
using System;
using System.Collections.Generic;

namespace EmailManager.Data.Contracts
{
    public interface IEmail
    {
        int Id { get; set; }
        string EmailId { get; set; }
        bool IsValid { get; set; }
        Loan Loan { get; set; }
        //int LoanId { get; set; }
        string ReceiveDate { get; set; }
        string Sender { get; set; }
        Status Status { get; set; }
        string Subject { get; set; }
        User User { get; set; }
        bool HasAttachments { get; set; }
        ICollection<Attachment> Attachments { get; set; }
    }
}