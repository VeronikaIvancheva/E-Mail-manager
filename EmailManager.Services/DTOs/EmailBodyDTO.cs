
using EmailManager.Data;

namespace EmailManager.Services.DTO
{
    public class EmailBodyDTO
    {
        public string UserId { get; set; }

        public string Body { get; set; }

        public Email Email { get; set; }

        public string EmailId { get; set; }
    }
}
