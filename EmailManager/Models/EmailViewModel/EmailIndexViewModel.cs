using System.Collections.Generic;

namespace EmailManager.Models.EmailViewModel
{
    public class EmailIndexViewModel
    {
        public IEnumerable<EmailViewModel> Emails { get; set; }

        public int? PreviousPage { get; set; }

        public int CurrentPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }
    }
}
