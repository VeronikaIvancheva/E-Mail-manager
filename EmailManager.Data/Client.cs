using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data
{
    public class Client
    {
        public int IdClient { get; set; }
        public string PhoneNumberClient { get; set; }

        public string EGNClient { get; set; }

        public string FirstNameClient { get; set; }
        public string LastNameClient { get; set; }
        public string EmailClient { get; set; }
    }
}
