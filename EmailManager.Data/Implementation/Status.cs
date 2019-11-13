using EmailManager.Data.Contracts;
using EmailManager.Data.Enums;
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
        public EmailStatus EmailStatus { get; set; }
        public DateTime NewStatus { get; set; }
        public DateTime LastStatus { get; set; }
        //DateTime на change на статуса
        public DateTime TimeStamp { get; set; }
        //Not sure about the type
        public string ActionTaken { get; set; }

        public ICollection<Email> Emails { get; set; }
    }
}
