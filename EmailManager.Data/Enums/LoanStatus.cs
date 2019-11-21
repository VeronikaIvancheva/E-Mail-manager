using System.ComponentModel.DataAnnotations;

namespace EmailManager.Data.Enums
{
    public enum LoanStatus
    {
        [Display(Name = "Approved")]
        Approved = 1,
        [Display(Name = "Rejected")]
        Rejected = 2
    }
}
