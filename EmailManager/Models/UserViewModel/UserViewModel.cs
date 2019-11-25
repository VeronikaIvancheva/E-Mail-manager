using EmailManager.Data.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models.UserViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
        }

        public UserViewModel(User user/*, Client client*/)
        {
            this.Name = user.Name;
            this.Role = user.Role;
            this.Email = user.Email;
            this.Id = user.Id;
            InitialRegistration = user.InitialRegistration;
            //this.HasLoan = client.Loans;
            //this.LoanSum = client.LoanedSum;
            //LastRegistration = user.LastRegistration;
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        [Display(Name = "Initial Registration")]
        public DateTime? InitialRegistration { get; set; }

        [Display(Name = "Last Registration")]
        public DateTime? LastRegistration { get; set; }

        public int LoanSum { get; set; }
        public ICollection<Loan> HasLoan { get; set; }
    }
}
