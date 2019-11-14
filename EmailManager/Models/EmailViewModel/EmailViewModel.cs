using EmailManager.Data;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using System;
using System.Collections.Generic;

namespace EmailManager.Models.EmailViewModel
{
    public class EmailViewModel
    {
        public EmailViewModel() { }

        public EmailViewModel(Email email)
        {
            this.Id = email.Id;
            this.EmailId = email.EmailId;
            this.Body = email.EmailBody.Body;
            this.EnumStatus = email.EnumStatus;
            //this.AttachmentsCount = email.Attachments
            this.Attachments = email.Attachments;
            this.Sender = email.Sender;
            this.Subject = email.Subject;
        }

        public int Id { get; set; }
        public int LoanId { get; set; }
        public string EmailId { get; set; }
        public string Sender { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailStatus EnumStatus { get; set; }

        public int AttachmentsCount { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
