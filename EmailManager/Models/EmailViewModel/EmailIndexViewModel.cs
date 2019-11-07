using System.Collections.Generic;

namespace EmailManager.Models.EmailViewModel
{
    public class EmailIndexViewModel
    {
        public IReadOnlyCollection<EmailViewModel> Emails { get; set; }
        public EmailViewModel EmailViewModel { get; set; }
    }
}
