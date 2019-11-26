using EmailManager.Data.Implementation;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmailManager.Models.UserViewModel
{
    public class UserViewModel
    {
        public UserViewModel() { }

        public UserViewModel(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Role = user.Role;
            this.Email = user.Email;
            this.InitialRegistration = user.InitialRegistration;
            this.LastRegistration = user.LastRegistration;
            this.LockOutEnd = user.LockoutEnd;
            this.UserName = user.UserName;
            this.PhoneNumber = user.PhoneNumber;
        }

        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTimeOffset? LockOutEnd { get; set; }
        public string Id { get; set; }
        [Display(Name = "Registration date")]
        public DateTime? InitialRegistration { get; set; }

        [Display(Name = "Last login")]
        public DateTime? LastRegistration { get; set; }
    }
}
