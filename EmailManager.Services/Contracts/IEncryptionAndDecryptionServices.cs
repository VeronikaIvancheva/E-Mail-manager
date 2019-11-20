using System.Collections.Generic;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IEncryptionAndDecryptionServices
    {
        string Base64Decrypt(string base64EncodedData);
        string Base64Encrypt(string plainText);
        Client DecodeLoanApplicant(Client client);
        string Decrypt(string cipherText);
        IEnumerable<Client> DecryptClientList(IEnumerable<Client> client);
        string Encrypt(string clearText);
        string ReplaceSign(string body);
    }
}