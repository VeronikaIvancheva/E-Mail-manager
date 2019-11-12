using EmailManager.Data.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    [Serializable]
    public class Attachment : IAttachment
    {
        [Key]
        public string AttachmentId { get; set; }
        public string FileName { get; set; }
        public double? AttachmentSize { get; set; }

        public string EmailId { get; set; }
        public Email Email { get; set; }
    }
}
