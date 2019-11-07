using EmailManager.Data.Implementation;
using System;

namespace EmailManager.Models.EmailViewModel
{
    public class LoanApplicationViewModel
    {
        public int Id { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public User User { get; set; }

        public EmailViewModel Email { get; set; }
    }
}
