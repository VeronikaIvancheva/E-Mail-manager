using EmailManager.Data.Implementation;
using System;

namespace EmailManager.Data.Contracts
{
    public interface ILoan
    {
        Client Client { get; set; }
        int ClientId { get; set; }
        DateTime DateAsigned { get; set; }
        decimal LoanedSum { get; set; }
        Email LoanEmail { get; set; }
        int LoanEmailId { get; set; }
        int LoanId { get; set; }
    }
}