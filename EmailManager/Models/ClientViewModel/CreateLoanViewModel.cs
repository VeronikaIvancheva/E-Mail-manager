using EmailManager.Data.Implementation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.ClientViewModel
{
    public class CreateLoanViewModel
    {
        [Required(ErrorMessage = "Please enter the client phone number.")]
        [MaxLength(10)]
        [MinLength(5)]
        [Display(Name = "Client phone number")]
        public string ClientPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter the client EGN.")]
        [MaxLength(10)]
        [MinLength(10)]
        [Display(Name = "Client EGN")]
        public string ClientEGN { get; set; }

        [Required(ErrorMessage = "Please enter the client first name.")]
        [Display(Name = "Client name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Please enter the client email.")]
        [Display(Name = "Client email")]
        public string ClientEmail { get; set; }

        public int LoanedSum { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
