using EmailManager.Models.ClientViewModel;
using EmailManager.Services.Contracts;
using EmailManager.Services.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailManager.Data.Implementation;

namespace EmailManager.Controllers
{
    public class ClientLoanController : Controller
    {
        private readonly ILoanServices _loanService;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ClientLoanController(ILoanServices loanService)
        {
            this._loanService = loanService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EmailLoanFill(ClientViewModel vm)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = new Client( vm.ClientName, vm.ClientPhoneNumber, vm.ClientEmail, vm.ClientEGN);

            try
            {
                await _loanService.CreateLoanApplication(client, userId);

            }
            catch (LoanExeptions ex)
            {
                log.Error($"Failed to create loan application. |{ex}| error");
                return RedirectToAction("Detail", new { id = client.ClientId });
            }

            return RedirectToAction("Detail", new { id = client.ClientId });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ApproveLoan(/*string approveData, string rejectData*/)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //int clientId = viewModel.ClientId;

            try
            {
                //string[] result = null;
                //if (approveData != null)
                //{
                //    result = approveData.Split(' ');
                //    var approveDto = new ApproveLoanDTO
                //    {
                //        Approved = result[0],
                //        EmailId = result[1],
                //        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                //    };

                //    await this._loanService.ApproveLoan(approveDto);
                //}
                //else if (rejectData != null)
                //{
                //    result = rejectData.Split(' ');
                //    var approveDto = new ApproveLoanDTO
                //    {
                //        Approved = result[0],
                //        EmailId = result[1],
                //        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                //    };

                //    await this._loanService.ApproveLoan(approveDto);
                //}
            }
            catch 
            {
                //log.Error($"Failed to create loan application. |{ex}| error");
                //return RedirectToAction("Detail", new { id = clientId });
                //throw new Exception("Loan was not approved");
            }

            return View();
        }

    }
}
