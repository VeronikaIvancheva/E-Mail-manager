using EmailManager.Data.Implementation;
using System;

namespace EmailManager.Data.Contracts
{
    public interface IEmail
    {
        int AttachmentCount { get; set; }
        double AttachmentSize { get; set; }
        string Body { get; set; }
        DateTime CurrentStatus { get; set; }
        int EmailId { get; set; }
        DateTime FirstRegistration { get; set; }
        bool IsValid { get; set; }
        Loan Loan { get; set; }
        int LoanId { get; set; }
        DateTime ReceiveDate { get; set; }
        string Sender { get; set; }
        Status Status { get; set; }
        string Subject { get; set; }
        DateTime TerminalStatus { get; set; }
        User User { get; set; }
    }
}