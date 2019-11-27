namespace EmailManager.Services.Contracts
{
    public interface IEncryptionServices
    {
        string Encrypt(string clearText);
        string Base64Encrypt(string plainText);
    }
}