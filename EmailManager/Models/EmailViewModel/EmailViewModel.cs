using EmailManager.Data;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using System;

namespace EmailManager.Models.EmailViewModel
{
    public class EmailViewModel
    {
        public EmailViewModel() { }

        public EmailViewModel(Email email, Attachment emailAttachments)
        {
            this.Id = email.Id;
            this.EmailId = email.EmailId;
            this.Body = email.EmailBody.Body;
            this.EnumStatus = email.EnumStatus;
            this.Sender = email.Sender;
            this.Subject = email.Subject;
            this.ReceiveDate = email.ReceiveDate;
            this.InCurrentStatusSince = email.Status.NewStatus;
            this.StatusChangedBy = email.User.UserName;
            this.CurrentUser = email.User.UserName;
            this.HasAttachments = email.HasAttachments;
            this.AttachmentName = emailAttachments.FileName;
            this.AttachmentSize = emailAttachments.AttachmentSizeKb;
        }

        public int Id { get; set; }
        public int LoanId { get; set; }
        public string EmailId { get; set; }
        public string Sender { get; set; }
        public string ReceiveDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailStatus EnumStatus { get; set; }
        public DateTime InCurrentStatusSince { get; set; }
        public string StatusChangedBy { get; set; }
        public string CurrentUser { get; set; }

        public bool HasAttachments { get; set; }
        public double? AttachmentSize { get; set; }
        public string AttachmentName { get; set; }
    }
}
