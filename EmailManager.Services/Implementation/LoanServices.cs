using EmailManager.Data;
using EmailManager.Data.Context;
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
        private readonly IDecryptionServices _decrypt;
        private readonly IEncryptionServices _encrypt;
        private readonly IEmailService _emailService;

        public LoanServices(EmailManagerContext context, IEncryptionServices securityEncrypt,
            IDecryptionServices decrypt, IEncryptionServices encrypt, IEmailService emailService)
        {
            this._context = context;
            this._securityEncrypt = securityEncrypt;
            this._decrypt = decrypt;
            this._encrypt = encrypt;
            this._emailService = emailService;
        }

        public async Task<Loan> CreateLoanApplication(Client client, int loanSum, string userId, int emailId)
        {
            #region Validations
            //if (client.ClientName == null || client.ClientEGN == null || client.ClientPhoneNumber == null)
            //{
            //    throw new LoanExeptions("Тhe details of the loan application have not been filled in correctly");
            //}

            //if (client.EmailId == null)
            //{
            //    throw new LoanExeptions($"Email with ID {client.EmailId} does not exist!");
            //}

            //if (client.ClientEGN.Length != 10)
            //{
            //    return new LoanExeptions("The EGN of the client must be exactly 10 digits!");
            //}

            //if (client.ClientName.Length < 3 || client.ClientName.Length > 50)
            //{
            //    throw new LoanExeptions("The length of the client's name is not correct!");
            //}
            //if (client.ClientPhoneNumber.Length < 3 || client.ClientPhoneNumber.Length > 10)
            //{
            //    throw new LoanExeptions("The length of the client's phone number is not correct!");
            //}

            //var isEgnCorrect = CheckEgnValidity(client.ClientEGN);

            //if (isEgnCorrect == false)
            //{
            //    throw new LoanExeptions("EGN must only contain digits");
            //}
            #endregion

            var email = await this._context.Emails
                .Where(e => e.Id == emailId)
                .FirstOrDefaultAsync();

            var createLoan = new Loan
            {
                LoanedSum = loanSum,
                DateAsigned = DateTime.UtcNow,
                ClientId = client.ClientId,
                Client = client,
                LoanEmailId = email.Id,
            };

            await _emailService.MarkOpenStatus(emailId, userId);

            await this._context.Loans.AddAsync(createLoan);
            await this._context.SaveChangesAsync();

            log.Info($"New loan to client Id: {client.ClientId} for {loanSum} sum.");

            return createLoan;
        }

        //public async Task<bool> ApproveLoan(ApproveLoanDTO approveLoanDto)
        //{

        //    if (approveLoanDto.EmailId == null || approveLoanDto.Approved == null)
        //    {
        //        throw new LoanExeptions($"Invalid loan request details");
        //    }

        //    int result = int.Parse(approveLoanDto.Approved);

        //    var loan = await this._context.Clients
        //        .Where(e => e.EmailId == approveLoanDto.EmailId)
        //        .FirstOrDefaultAsync();

        //    var email = await this._context.Emails
        //        .Where(e => e.EmailId == approveLoanDto.EmailId)
        //        .FirstOrDefaultAsync();

        //    var user = await this._context.Users
        //        .Where(id => id.Id == approveLoanDto.UserId)
        //        .SingleOrDefaultAsync();

        //    if (result == (int)EmailStatus.Rejected)
        //    {
        //        loan.IsApproved = false;
        //        loan.User = user;
        //        log.Info("Loan was rejected");
        //    }

        //    if (result == (int)EmailStatus.Approved)
        //    {
        //        loan.IsApproved = true;
        //        loan.User = user;
        //        log.Info("Loan was approved");
        //    }

        //    email.SetCurrentStatus = DateTime.Now;
        //    email.EmailStatusId = (int)EmailStatus.Closed;
        //    email.IsDeleted = DateTime.Now;

        //    await this._context.SaveChangesAsync();

        //    return true;
        //}

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

        public Client DecryptClientInfo(Client clientId)
        {
            Client client = _context.Clients
                .FirstOrDefault(c => c.ClientId == clientId.ClientId);

            string decryptName = _decrypt.Decrypt(client.ClientName);
            string decryptEgn = _decrypt.Decrypt(client.ClientEGN);
            string decryptPhoneNumber = _decrypt.Decrypt(client.ClientPhoneNumber);
            string decryptEmail = _decrypt.Decrypt(client.ClientEmail);

            Client clientForLoan = new Client
            {
                ClientName = decryptName,
                ClientEmail = decryptEgn,
                ClientEGN = decryptPhoneNumber,
                ClientPhoneNumber = decryptEmail,
                UserId = client.UserId,
                EmailId = client.EmailId,
                Email = client.Email,
            };

            return client;
        }

        public Client EncryptClientInfo(string clientName, string clientPhone, string clientEGN,
            string clientEmail, string userId)
        {
            var encryptedName = this._encrypt.Encrypt(clientName);
            var encryptedEgn = this._encrypt.Encrypt(clientPhone);
            var encryptedPhoneNumber = this._encrypt.Encrypt(clientEGN);
            var encryptedEmail = this._encrypt.Encrypt(clientEmail);

            var newClient = new Client
            {
                ClientName = encryptedName,
                ClientEmail = encryptedEgn,
                ClientEGN = encryptedPhoneNumber,
                ClientPhoneNumber = encryptedEmail,
                UserId = userId,
            };

            return newClient;
        }

        public async Task<Client> AddClient(string clientName, string clientPhone, string clientEGN,
            string clientEmail, string userId)
        {
            IEnumerable<Client> clientAll = _context.Clients;
            IEnumerable<Client> decryptedClients = _decrypt.DecryptClientList(clientAll);

            Client newClientCheck = decryptedClients
                .FirstOrDefault(a => a.ClientEGN == clientEGN);

            if (newClientCheck == null)
            {
                newClientCheck = EncryptClientInfo(clientName, clientPhone, clientEGN, clientEmail, userId);

                await this._context.Clients.AddAsync(newClientCheck);
                await this._context.SaveChangesAsync();

                log.Info($"New client with Id: {newClientCheck.ClientId}");
            }
            else
            {
                var decryptClientData = DecryptClientInfo(newClientCheck);
                newClientCheck = decryptClientData;

                log.Info($"Found excisting client with Id {newClientCheck.ClientId}");
            }

            if (newClientCheck == null)
            {
                log.Fatal("New client is null! Must never reach here! Error in AddClient method in Loan service.");
            }

            return newClientCheck;
        }
    }
}
