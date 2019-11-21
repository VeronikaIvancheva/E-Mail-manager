using System;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    [Serializable]
    public class Attachment 
    {
        [Key]
        public string AttachmentId { get; set; }
        public string FileName { get; set; }
        public double? AttachmentSizeKb { get; set; }

        public string EmailId { get; set; }
        public Email Email { get; set; }
    }
}
