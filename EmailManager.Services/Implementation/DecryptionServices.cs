using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EmailManager.Services.Implementation
{
    public class DecryptionServices : IDecryptionServices
    {
        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "banana";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = key.GetBytes(32);
                encryptor.IV = key.GetBytes(16);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(stream, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(stream.ToArray());
                }
                //key.Dispose();
            }
            return cipherText;
        }

        public Client DecodeClient(Client client)
        {
            var name = Decrypt(client.ClientName);
            var egn = Decrypt(client.ClientEGN);
            var phoneNumber = Decrypt(client.ClientPhoneNumber);

            var applicant = new Client
            {
                ClientName = name,
                ClientEGN = egn,
                ClientPhoneNumber = phoneNumber,
                ClientEmail = client.ClientEmail
            };

            return applicant;
        }

        public IEnumerable<Client> DecryptClientList(IEnumerable<Client> client)
        {
            var clients = new List<Client>();

            foreach (var item in client)
            {
                var egn = Decrypt(item.ClientEGN);
                clients.Add(new Client { ClientEGN = egn, ClientId = item.ClientId });
            }
            return clients;
        }

        public string ReplaceSign(string body)
        {
            string codedBody = body.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");

            return codedBody;
        }
    }
}
