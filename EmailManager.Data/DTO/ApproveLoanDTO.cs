using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data.DTO
{
    public class ApproveLoanDTO
    {
        public string UserId { get; set; }

        public string Approved { get; set; }

        public string EmailId { get; set; }
    }
}
