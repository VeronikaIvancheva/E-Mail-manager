using EmailManager.Data.Context;
using EmailManager.Data.DTO;
using EmailManager.Data.Enums;
using EmailManager.Data.Implementation;
using EmailManager.Services.Exeptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailManager.Services.Implementation
{
    public class LoanServices
    {
        private readonly EmailManagerContext _context;
        private readonly EncryptionAndDecryptionServices _security;

        public LoanServices(EmailManagerContext context, EncryptionAndDecryptionServices security)
        {
            this._context = context;
            this._security = security;
        }

        public async Task<Client> ClientLoanApplication(ClientDTO clientDto)
        {
            var validationMethod = ValidationMethod(clientDto);

            var user = await this._context.Users
                .Include(l => l.Clients)
                .Where(userId => userId.Id == validationMethod.UserId)
                .FirstOrDefaultAsync();


            var encodeName = this._security.Encrypt(validationMethod.ClientName);
            var encodeEGN = this._security.Encrypt(validationMethod.ClientEGN);
            var encodePhoneNumber = this._security.Encrypt(validationMethod.ClientPhoneNumber);


            var email = await this._context.Emails
                .Where(e => e.EmailId == validationMethod.EmailId)
                .SingleOrDefaultAsync();

            var application = new Client
            {
                ClientName = encodeName,
                ClientEGN = encodeEGN,
                ClientPhoneNumber = encodePhoneNumber,
                UserId = validationMethod.UserId,
                User = user,
                Email = email,
                EmailId = validationMethod.EmailId
            };

            email.SetCurrentStatus = DateTime.Now;
            email.EmailStatusId = (int)EmailStatus.Open;

            await this._context.Clients.AddAsync(application);
            await this._context.SaveChangesAsync();
            return application;
        }
        public async Task<bool> ApproveLoanAsync(ApproveLoanDTO approveLoanDto)
        {

            if (approveLoanDto.EmailId == null || approveLoanDto.Approved == null)
            {
                throw new LoanExeptions($"Invalid loan request details");
            }

            int expectedResult = int.Parse(approveLoanDto.Approved);

            var loan = await this._context.Clients
                .Where(e =>e.EmailId == approveLoanDto.EmailId)
                .FirstOrDefaultAsync();

            var email = await this._context.Emails
            .Where(e => e.EmailId == approveLoanDto.EmailId)
            .FirstOrDefaultAsync();

            var user = await this._context.Users
                .Where(id => id.Id == approveLoanDto.UserId)
                .SingleOrDefaultAsync();

            if (expectedResult == (int)LoanStatus.Rejected)
            {
                loan.IsApproved = false;
                loan.User = user;
            }

            if (expectedResult == (int)LoanStatus.Approved)
            {
                loan.IsApproved = true;
                loan.User = user;
            }

            email.SetCurrentStatus = DateTime.Now;
            email.EmailStatusId = (int)EmailStatus.Closed;
            email.IsDeleted = DateTime.Now;

            await this._context.SaveChangesAsync();

            return true;
        }
        public ClientDTO ValidationMethod(ClientDTO clientDto)
        {
            if (clientDto.ClientName == null ||clientDto.ClientEGN == null|| clientDto.ClientPhoneNumber == null)
            {
                throw new LoanExeptions("Тhe details of the loan request have not been filled in correctly");
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
            if (clientDto.ClientPhoneNumber.Length < 3 || clientDto.ClientPhoneNumber.Length > 50)
            {
                throw new LoanExeptions("The length of the client's phone number is not correct!");
            }

            return clientDto;
        }
    }
}
