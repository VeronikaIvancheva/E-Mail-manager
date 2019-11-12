using System;

namespace EmailManager.Data.Utilities
{
    public interface ILogger
    {
        DateTime CreatedOn { get; set; }
        DateTime CurrentStatus { get; set; }
        DateTime ReceiveDate { get; set; }
        DateTime TerminalStatus { get; set; }
    }
}