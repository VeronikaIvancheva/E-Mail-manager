using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Utilities
{
    interface IAuditableStatus
    {
        DateTime NewStatus { get; set; }
        DateTime LastStatus { get; set; }
        DateTime TimeStamp { get; set; }
    }
}
