using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models.UserViewModel
{
    public class UserIndexViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }


        public int? PreviousPage { get; set; }

        public int CurrentPage { get; set; }

        public int? NextPage { get; set; }

        public int TotalPages { get; set; }
    }
}
