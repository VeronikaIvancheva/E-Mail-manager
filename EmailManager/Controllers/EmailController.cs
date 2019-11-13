using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailManager.Models.EmailViewModel;
using EmailManager.Services.Contracts;
using EmailManager.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace EmailManager.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService service)
        {
            this._emailService = service;
        }

        public async Task<IActionResult> Index()
        {
            var emailsAllResults = await _emailService.GetAllEmails();

            var emailsListing = emailsAllResults
                .Select(e => new EmailViewModel
                {
                    LoanId = e.LoanId,
                    Subject = e.Subject,
                    Sender = e.Sender,
                    Body = e.EmailBody.Body,
                    AttachmentsCount = e.Attachments.Count,
                    ReceiveDate = e.ReceiveDate,
                    EnumStatus = _emailService.GetStatus(e.EmailId),
                })
                .OrderBy(t => t.ReceiveDate)
                .ToList();

            var emailModel = new EmailIndexViewModel
            {
                Emails = emailsListing
            };

            return View(emailModel);
        }

        public IActionResult Detail(int id)
        {
            var email = _emailService.GetEmail(id);
            var emailModel = new EmailViewModel(email);

            return View("Detail", emailModel);
        }

        [HttpPost]
        public async Task<IActionResult> MarkNew(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int loanId = viewModel.LoanId;

            try
            {
                var markNew = _emailService.MarkNewStatus(loanId, userId);
            }
            catch (Exception ex)
            {
                TempData["markNewError"] = ex.Message;

                return RedirectToAction("Index", new { id = loanId });
            }

            return RedirectToAction("Index", new { id = loanId });
        }

        //[HttpPost]
        //public async Task<IActionResult> MarkClosed(int id)
        //{

        //}

        //[HttpPost]
        //public async Task<IActionResult> MarkInvalid(int id)
        //{

        //}

        //[HttpPost]
        //public async Task<IActionResult> MarkOpen(int id)
        //{

        //}

        //[HttpPost]
        //public async Task<IActionResult> MarkNotReviewed(int id)
        //{

        //}
    }
}