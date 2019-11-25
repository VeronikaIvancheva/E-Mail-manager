using EmailManager.Models.ClientViewModel;
using EmailManager.Services.Contracts;
using EmailManager.Services.Exeptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailManager.Data.Implementation;
using EmailManager.Data;
using EmailManager.Models.EmailViewModel;

namespace EmailManager.Controllers
{
    public class ClientLoanController : Controller
    {
        private readonly ILoanServices _loanService;
        private readonly IEmailService _emailService;
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ClientLoanController(ILoanServices loanService, IEmailService emailService)
        {
            this._loanService = loanService;
            this._emailService = emailService;
        }

        public IActionResult GetEmailDetails(int id)
        {
            var email = _emailService.GetEmail(id);
            var emailAttachments = _emailService.GetAttachment(id);

            var emailModel = new EmailViewModel(email, emailAttachments);

            log.Info($"User opened email detail page. Email Id: {id}");

            return View("CreateLoan", emailModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLoan(CreateLoanViewModel vm)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var clientCheck = await _loanService.AddClient(vm.ClientName, vm.ClientPhoneNumber, vm.ClientEGN, vm.ClientEmail, userId);

            try
            {
                await _loanService.CreateLoanApplication(clientCheck, vm.LoanSum, userId, vm.EmailDbId);

            }
            catch (LoanExeptions ex)
            {
                log.Error($"Failed to create loan application. |{ex}| error");
                return RedirectToAction("ListAllStatusEmails", "Email");
            }

            return RedirectToAction("ListAllStatusEmails", "Email");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
