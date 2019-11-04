using EmailManager.Data.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Implementation
{
    public class Loan : ILoan
    {
        [Key]
        public int LoanId { get; set; }
        public decimal LoanedSum { get; set; }
        public DateTime DateAsigned { get; set; }

        public int LoanEmailId { get; set; }
        public Email LoanEmail { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

    }
}
