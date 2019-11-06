using EmailManager.Data.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    [Serializable]
    public class Attachment : IAttachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public double? AttachmentSize { get; set; }

        public int? EmailId { get; set; }
        public Email Email { get; set; }
    }
}
