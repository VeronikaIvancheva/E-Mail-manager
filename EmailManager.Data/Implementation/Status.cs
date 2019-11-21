using EmailManager.Data.Enums;
using EmailManager.Data.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    public class Status : IAuditableStatus
    {
        public Status()
        {
            this.Emails = new HashSet<Email>();
        }

        [Key]
        public int StatusID { get; set; }
        [Display(Name = "Status")]
        public EmailStatus EmailStatus { get; set; }
        //Not sure about the type
        [Display(Name = "Action")]
        public string ActionTaken { get; set; }
        public ICollection<Email> Emails { get; set; }

        [Display(Name = "New status since")]
        public DateTime NewStatus { get; set; }
        [Display(Name = "Last status since")]
        public DateTime LastStatus { get; set; }
        //DateTime на change на статуса
        [Display(Name = "Last status change since")]
        public DateTime TimeStamp { get; set; }
    }
}
