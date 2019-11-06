using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Contracts
{
    public interface IAttachment
    {
        int AttachmentId { get; set; }
        string FileName { get; set; }
        double? AttachmentSize { get; set; }

        int? EmailId { get; set; }
        Email Email { get; set; }
    }
}
