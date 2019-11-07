using EmailManager.Data.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models.ClientViewModel
{
    public class ClientViewModel
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Please enter the client phone number.")]
        [MaxLength(10)]
        [Display(Name = "Client phone number")]
        public string ClientPhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter the client EGN.")]
        [MaxLength(10)]
        [Display(Name = "Client EGN")]
        public string ClientEGN { get; set; }

        [Required(ErrorMessage = "Please enter the client first name.")]
        [Display(Name = "Client first name")]
        public string ClientFirstName { get; set; }

        [Required(ErrorMessage = "Please enter the client last name.")]
        [Display(Name = "Client last name")]
        public string ClientLastName { get; set; }

        [Required(ErrorMessage = "Please enter the client email.")]
        [Display(Name = "Client email")]
        public string ClientEmail { get; set; }

        public ICollection<Loan> Loans { get; set; }
    }
}
