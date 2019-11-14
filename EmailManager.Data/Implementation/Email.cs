using EmailManager.Data.Contracts;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data
{
    [Serializable]
    public class Email : IEmail
    {
        public Email()
        {
            this.Attachments = new List<Attachment>();
        }

        [Key]
        public int Id { get; set; }
        public string EmailId { get; set; }
        public bool IsValid { get; set; }
        public string Sender { get; set; }
        public string ReceiveDate { get; set; }
        public string Subject { get; set; }
        public EmailStatus EnumStatus { get; set; }
        public DateTime FirstRegistration { get; set; }
        //questionable
        public DateTime CurrentStatus { get; set; }
        public DateTime TerminalStatus { get; set; }
        //public int LoanId { get; set; }
        public Loan Loan { get; set; }
        public EmailBody EmailBody { get; set; }
        public User User { get; set; }

        public Status Status { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
