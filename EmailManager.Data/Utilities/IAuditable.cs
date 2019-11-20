using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Utilities
{
    interface IAuditable
    {
        DateTime? InitialRegistration { get; set; }

        DateTime? SetCurrentStatus { get; set; }

        DateTime? SetTerminalState { get; set; }
    }
}
