using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data
{
    public class Client : User
    {
        public override string PhoneNumber { get; set; }

        public string EGN { get; set; }
    }
}
