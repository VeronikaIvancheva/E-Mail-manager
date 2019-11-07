using EmailManager.Data.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models.EmailViewModel
{
    public class EmailViewModel
    {
        public int EmailId { get; set; }
        public string Sender { get; set; }
        public DateTime ReceiveDate { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public ICollection<Attachment> Attachments { get; set; }
    }
}
