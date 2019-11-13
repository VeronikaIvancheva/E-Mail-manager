using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Models.EmailViewModel;
using EmailManager.Services.Contracts;
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
    }
}