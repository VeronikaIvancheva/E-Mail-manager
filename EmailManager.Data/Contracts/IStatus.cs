using EmailManager.Data.Enums;
using System;

namespace EmailManager.Data.Contracts
{
    public interface IStatus
    {
        string ActionTaken { get; set; }
        DateTime LastStatus { get; set; }
        DateTime NewStatus { get; set; }
        int StatusID { get; set; }
        DateTime TimeStamp { get; set; }
        EmailStatus EmailStatus { get; set; }
    }
}