using EmailManager.Data.Contracts;
using System;

namespace EmailManager.Data.Implementation
{
    public class Email : IEmail
    {
        public int EmailId { get; set; }
        public bool IsValid { get; set; }
        public string Sender { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int AttachmentCount { get; set; }
        public double AttachmentSize { get; set; }
        public DateTime FirstRegistration { get; set; }
        //questionable
        public DateTime CurrentStatus { get; set; }
        public DateTime TerminalStatus { get; set; }

        public int LoanId { get; set; }
        public Loan Loan { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int StatusId { get; set; }
        public Status Status { get; set; }

    }
}
