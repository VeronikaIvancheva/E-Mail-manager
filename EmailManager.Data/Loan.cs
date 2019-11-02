using System;
using System.Collections.Generic;
using System.Text;

namespace EmailManager.Data
{
    public class Loan
    {
        public int LoanId { get; set; }
        public Client Client { get; set; }
        public decimal LoanedSum { get; set; }
        public DateTime DateAsigned { get; set; }

    }
}
