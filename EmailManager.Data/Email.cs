using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data
{
    public class Email
    {
        public int EmailId { get; set; }
        public bool IsValid { get; set; }
        public User User { get; set; }
        public Status EmailStatus { get; set; }
        //Sender
        public Client ClientId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int AttachmentCount { get; set; }
        public double AttachmentSize { get; set; }
        public DateTime FirstRegistration { get; set; }
        //questionable
        public DateTime CurrentStatus { get; set; }
        public DateTime TerminalStatus { get; set; }


    }
}
