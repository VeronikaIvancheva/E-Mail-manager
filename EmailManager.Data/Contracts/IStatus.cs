using System;

namespace EmailManager.Data.Contracts
{
    public interface IStatus
    {
        string ActionTaken { get; set; }
        string Closed { get; set; }
        string InitialApplication { get; set; }
        DateTime LastStatus { get; set; }
        string New { get; set; }
        DateTime NewStatus { get; set; }
        string NotReviewed { get; set; }
        string Opened { get; set; }
        int StatusID { get; set; }
        DateTime TimeStamp { get; set; }
    }
}