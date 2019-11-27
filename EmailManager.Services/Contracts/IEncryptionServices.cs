using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IEncryptionServices
    {
        string Encrypt(string clearText);
        string Base64Encrypt(string plainText);
        Client EncryptClientInfo(string clientName, string clientPhone, string clientEGN,
           string clientEmail, string userId);
    }
}