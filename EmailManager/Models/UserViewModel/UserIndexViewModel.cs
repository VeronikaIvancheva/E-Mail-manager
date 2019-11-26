using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailManager.Models.UserViewModel
{
    public class UserIndexViewModel
    {
        public UserIndexViewModel() { }

        public UserIndexViewModel(IEnumerable<UserViewModel> users)
        {
            Users = users;
        }

        public IEnumerable<UserViewModel> Users { get; set; }

    }
}
