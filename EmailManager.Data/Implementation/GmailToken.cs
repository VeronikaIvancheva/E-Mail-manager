using System;

namespace EmailManager.Data.Implementation
{
    public class GmailToken
    {
        public int GmailTokenId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
