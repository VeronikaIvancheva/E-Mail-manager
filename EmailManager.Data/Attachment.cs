using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailManager.Data
{
    [Serializable]
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public double? AttachmentSize { get; set; }

        public int EmailId { get; set; }
        public Email Email { get; set; }
    }
}
