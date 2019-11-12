using EmailManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.Implementation
{
    public class EmailBody : IEmailBody
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public Email Email { get; set; }
    }
}
