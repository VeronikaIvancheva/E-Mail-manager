namespace EmailManager.Services.Contracts
{
    public interface IEncryptionServices
    {
        string Encrypt(string clearText);
    }
}