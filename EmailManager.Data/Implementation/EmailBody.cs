using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailManager.Data.Implementation
{
    public class EmailBody 
    {
        [Key]
        public string EmailId { get; set; }
        public string UserId { get; set; }
        public string Body { get; set; }
        public Email Email { get; set; }
        
    }
}
