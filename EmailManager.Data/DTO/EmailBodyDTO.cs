using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.DTO
{
    public class EmailBodyDTO
    {
        public string UserId { get; set; }

        public string Body { get; set; }

        public Email Email { get; set; }

        public string EmailId { get; set; }
    }
}
