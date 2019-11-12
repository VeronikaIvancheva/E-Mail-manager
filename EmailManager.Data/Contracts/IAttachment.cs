using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Contracts
{
    public interface IAttachment
    {
        string AttachmentId { get; set; }
        string FileName { get; set; }
        double? AttachmentSize { get; set; }

        string EmailId { get; set; }
        Email Email { get; set; }
    }
}
