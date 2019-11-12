using EmailManager.Data.Contracts;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using EmailManager.Data.Utilities;
using System;
using System.Collections.Generic;

namespace EmailManager.Data
{
    public class Email : IEmail,ILogger
    {
        public Email()
        {
            this.Attachments = new List<Attachment>();
        }

        public int EmailId { get; set; }
        public bool IsValid { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
        public int LoanId { get; set; }
        public Loan Loan { get; set; }
        public User User { get; set; }
        public EmailStatus Status { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime CurrentStatus { get; set; }
        public DateTime ReceiveDate { get; set; }
        public DateTime TerminalStatus { get; set; }
    }
}
