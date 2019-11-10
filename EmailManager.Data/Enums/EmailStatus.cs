using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EmailManager.Data.Enums
{
   public enum EmailStatus
    {
        [Display(Name = "Not Reviewed")]
        NotReviewed = 1,
        [Display(Name = "Not Valid")]
        NotValid = 2,
        [Display(Name = "New")]
        New = 3,
        [Display(Name = "Open")]
        Open = 4,
        [Display(Name = "Closed")]
        Closed = 5
    }
}
