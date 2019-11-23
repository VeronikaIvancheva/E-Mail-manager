using System.Collections.Generic;

namespace EmailManager.Models.EmailViewModel
{
    public class EmailIndexViewModel
    {
        public EmailIndexViewModel() { }

        public EmailIndexViewModel(IEnumerable<EmailViewModel> emails, int currentPage, int totalPages)
        {
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.Emails = emails;
        }

        public IEnumerable<EmailViewModel> Emails { get; set; }

        public int? PreviewPage { get; set; }

        public int CurrentPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }
    }
}
