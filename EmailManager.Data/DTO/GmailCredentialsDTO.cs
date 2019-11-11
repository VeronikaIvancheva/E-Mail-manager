using Newtonsoft.Json;

namespace EmailManager.Data.DTO
{
    public class GmailCredentialsDTO
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresInSeconds { get; set; }
    }
}
