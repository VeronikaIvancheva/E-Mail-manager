using EmailManager.Data.Contracts;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using EmailManager.Data.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data
{
    [Serializable]
    public class Email : IEmail,IAuditableRegistration,IDeletable
    {
        public Email()
        {
            this.Attachments = new HashSet<Attachment>();
        }

        [Key]
        public int Id { get; set; }
        public string EmailId { get; set; }
        public bool IsValid { get; set; }
        public string Sender { get; set; }
        public string ReceiveDate { get; set; }
        public string Subject { get; set; }
        public EmailStatus EnumStatus { get; set; }
        public int EmailStatusId { get; set; } = (int)EmailStatus.NotReviewed;
        public Loan Loan { get; set; }
        public EmailBody EmailBody { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public bool IsSeen { get; set; }
        public Status Status { get; set; }
        public bool HasAttachments { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
        public DateTime? InitialRegistration { get; set; }
        public DateTime? SetCurrentStatus { get; set; }
        public DateTime? IsDeleted { get; set; }
        public DateTime DeletionDate { get; set; }
        public DateTime? LastRegistration { get; set; }
    }
}
