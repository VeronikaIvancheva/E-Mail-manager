using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmailManager.Data
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public Client Client { get; set; }
        public decimal LoanedSum { get; set; }
        public DateTime DateAsigned { get; set; }
        public Email LoanEmail { get; set; }
        public int LoanEmailID { get; set; }

    }
}
