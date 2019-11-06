using EmailManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    public class Status : IStatus
    {
        public Status()
        {
            this.Emails = new List<Email>();
        }

        [Key]
        public int StatusID { get; set; }
        public string New { get; set; }
        public string Opened { get; set; }
        public string Closed { get; set; }
        public string NotReviewed { get; set; }
        public string InitialApplication { get; set; }
        public DateTime NewStatus { get; set; }
        public DateTime LastStatus { get; set; }
        public DateTime TimeStamp { get; set; }
        //Not sure about the type
        public string ActionTaken { get; set; }

        public ICollection<Email> Emails { get; set; }
    }
}
