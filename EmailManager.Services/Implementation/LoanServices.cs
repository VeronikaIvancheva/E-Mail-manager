using EmailManager.Data.Context;
using EmailManager.Services.DTO;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using EmailManager.Services.Contracts;
using EmailManager.Services.Exeptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class LoanServices : ILoanServices
    {
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly EmailManagerContext _context;
        private readonly IEncryptionServices _securityEncrypt;
        private readonly DecryptionServices _decrypt;
        private readonly EncryptionServices _encrypt;

        public LoanServices(EmailManagerContext context, IEncryptionServices securityEncrypt, DecryptionServices decrypt, EncryptionServices encrypt)
        {
            this._context = context;
            this._securityEncrypt = securityEncrypt;
            this._decrypt = decrypt;
            this._encrypt = encrypt;
        }

        public async Task<Client> ClientLoanApplication(ClientDTO clientDto)
        {

            if (clientDto.ClientName == null || clientDto.ClientEGN == null || clientDto.ClientPhoneNumber == null)
            {
                throw new LoanExeptions("Тhe details of the loan application have not been filled in correctly");
            }

            if (clientDto.EmailId == null)
            {
                throw new LoanExeptions($"Email with ID {clientDto.EmailId} does not exist!");
            }

            if (clientDto.ClientEGN.Length != 10)
            {
                throw new LoanExeptions("The EGN of the client must be exactly 10 digits!");
            }

            if (clientDto.ClientName.Length < 3 || clientDto.ClientName.Length > 50)
            {
                throw new LoanExeptions("The length of the client's name is not correct!");
            }
            if (clientDto.ClientPhoneNumber.Length < 3 || clientDto.ClientPhoneNumber.Length > 10)
            {
                throw new LoanExeptions("The length of the client's phone number is not correct!");
            }

            var isEgnCorrect = CheckEgnValidity(clientDto.ClientEGN);

            if (isEgnCorrect == false)
            {
                throw new LoanExeptions("EGN must only contain digits");
            }

            var user = await this._context.User
                .Include(l => l.Clients)
                .Where(userId => userId.Id == clientDto.UserId)
                .FirstOrDefaultAsync();

            var encryptedName = this._encrypt.Encrypt(clientDto.ClientName);
            var encryptedEgn = this._encrypt.Encrypt(clientDto.ClientEGN);
            var encryptedPhoneNumber = this._encrypt.Encrypt(clientDto.ClientPhoneNumber);
            var encryptedEmail = this._encrypt.Encrypt(clientDto.EmailId);

            var email = await this._context.Emails
                .Where(e => e.EmailId == clientDto.EmailId)
                .SingleOrDefaultAsync();

            var loan = new Client
            {
                ClientName = encryptedName,
                ClientEGN = encryptedEgn,
                ClientPhoneNumber = encryptedPhoneNumber,
                UserId = clientDto.UserId,
                User = user,
                Email = email,
                EmailId = encryptedEmail
            };

            email.SetCurrentStatus = DateTime.Now;
            email.EmailStatusId = (int)EmailStatus.Open;

            await this._context.Clients.AddAsync(loan);
            await this._context.SaveChangesAsync();
            return loan;
        }

       
        
        public async Task<bool> ApproveLoanAsync(ApproveLoanDTO approveLoanDto)
        {

            if (approveLoanDto.EmailId == null || approveLoanDto.Approved == null)
            {
                throw new LoanExeptions($"Invalid loan request details");
            }

            int result = int.Parse(approveLoanDto.Approved);

            var loan = await this._context.Clients
                .Where(e => e.EmailId == approveLoanDto.EmailId)
                .FirstOrDefaultAsync();

            var email = await this._context.Emails
            .Where(e => e.EmailId == approveLoanDto.EmailId)
            .FirstOrDefaultAsync();

            var user = await this._context.Users
                .Where(id => id.Id == approveLoanDto.UserId)
                .SingleOrDefaultAsync();

            if (result == (int)EmailStatus.Rejected)
            {
                loan.IsApproved = false;
                loan.User = user;
                log.Info("Loan was rejected");
            }

            if (result == (int)EmailStatus.Approved)
            {
                loan.IsApproved = true;
                loan.User = user;
                log.Info("Loan was approved");

            }

            email.SetCurrentStatus = DateTime.Now;
            email.EmailStatusId = (int)EmailStatus.Closed;
            email.IsDeleted = DateTime.Now;

            await this._context.SaveChangesAsync();

            return true;
        }
        public bool CheckEgnValidity(string email)
        {
            var egn = "";

            for (int i = 0; i < email.Length; i++)
            {
                if (Char.IsDigit(email[i]))
                {
                    egn.Concat(email[i].ToString());
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public Client GetClient(int clientID)
        {
            var client = _context.Clients.FirstOrDefault(c=>c.ClientId==clientID);

            var decryptName = _decrypt.Decrypt(client.ClientName);
            var decryptEgn = _decrypt.Decrypt(client.ClientEGN);
            var decryptPhoneNumber = _decrypt.Decrypt(client.ClientPhoneNumber);
            var decryptEmail = _decrypt.Decrypt(client.EmailId);

            return client;

        }
       
    }
}
