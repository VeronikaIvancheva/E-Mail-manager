namespace EmailManager.Services.Contracts
{
    public interface IEncryptionServices
    {
        string Base64Encrypt(string plainText);
        string Encrypt(string clearText);
    }
}