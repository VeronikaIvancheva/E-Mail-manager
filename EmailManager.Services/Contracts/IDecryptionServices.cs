using System.Collections.Generic;
using EmailManager.Data.Implementation;

namespace EmailManager.Services.Contracts
{
    public interface IDecryptionServices
    {
        string Decrypt(string cipherText);
        IEnumerable<Client> DecryptClientList(IEnumerable<Client> client);
    }
}