using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Utilities
{
    interface IAuditableRegistration
    {
        DateTime? InitialRegistration { get; set; }

        DateTime? SetCurrentStatus { get; set; }

        
    }
}
