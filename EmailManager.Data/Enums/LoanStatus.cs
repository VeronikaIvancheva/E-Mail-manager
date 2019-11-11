using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailManager.Data.Enums
{
    public enum LoanStatus
    {
        [Display(Name = "Approved")]
        Approved = 1,
        [Display(Name = "Rejected")]
        Rejected = 2,
        [Display(Name = "NotReviewed")]
        NotReviewed,

    }
}
