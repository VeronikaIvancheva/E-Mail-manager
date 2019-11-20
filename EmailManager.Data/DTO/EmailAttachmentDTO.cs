using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.DTO
{
   public class EmailAttachmentDTO
    {
        public string FileName { get; set; }

        public double? AttachmentSizeKb { get; set; }

        public string EmailId { get; set; }
    }
}
