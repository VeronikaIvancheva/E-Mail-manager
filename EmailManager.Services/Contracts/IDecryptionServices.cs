using System.Collections.Generic;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IDecryptionServices
    {
        Client DecodeClient(Client client);
        string Decrypt(string cipherText);
        IEnumerable<Client> DecryptClientList(IEnumerable<Client> client);
        string ReplaceSign(string body);
    }
}