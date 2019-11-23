using EmailManager.Data.DTO;
using EmailManager.Services.Contracts;
using EmailManager.Services.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmailManager.Controllers
{
    public class ClientLoanController : Controller
    {
        private readonly ILoanServices _loanService;

        public ClientLoanController(ILoanServices loanService)
        {
            this._loanService = loanService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EmailLoanFill(string name, string egn, string phoneNumber, string email)
        {
            try
            {
                var loanApplication = new ClientDTO
                {
                    ClientName = name,
                    ClientEGN = egn,
                    ClientPhoneNumber = phoneNumber,
                    EmailId = email,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

            }
            catch (LoanExeptions)
            {
                return Json(new { exeption = email });
            }

            return Json(new { exeption = email });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ApproveLoan(string approveData, string rejectData)
        {
            string[] result = null;
            try
            {
                if (approveData != null)
                {
                    result = approveData.Split(' ');
                    var approveDto = new ApproveLoanDTO
                    {
                        Approved = result[0],
                        EmailId = result[1],
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    };

                    await this._loanService.ApproveLoanAsync(approveDto);
                }
                else if (rejectData != null)
                {
                    result = rejectData.Split(' ');
                    var approveDto = new ApproveLoanDTO
                    {
                        Approved = result[0],
                        EmailId = result[1],
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    };

                    await this._loanService.ApproveLoanAsync(approveDto);
                }
            }
            catch (LoanExeptions ex)
            {
                throw new Exception("Loan was not approved");
            }

            return Json(new { emailId = result[1] });
        }

    }
}
