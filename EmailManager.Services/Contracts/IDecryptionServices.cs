using System.Collections.Generic;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IDecryptionServices
    {
        string Base64Decrypt(string base64EncodedData);
        Client DecodeClient(Client client);
        string Decrypt(string cipherText);
        IEnumerable<Client> DecryptClientList(IEnumerable<Client> client);
        string ReplaceSign(string body);
    }
}