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
                    Id = e.Id,
                    Subject = e.Subject,
                    Sender = e.Sender,
                    Body = e.EmailBody.Body,
                    HasAttachments = e.HasAttachments,
                    ReceiveDate = e.ReceiveDate,
                    EnumStatus = _emailService.GetStatus(e.EmailId),
                })
                .OrderBy(t => t.Id)
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

        // Prerabotete go v async 
        [HttpPost]
        public async Task<IActionResult> MarkNew(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            await _emailService.MarkNewStatus(emailId, userId);
            return RedirectToAction("Index", new { id = emailId });
            // try
            // {
            //    await _emailService.MarkNewStatus(emailId, userId);

            //     //if (markNew == false)
            //     //{
            //     //    throw new ArgumentException("You cannot change the status because it is already changed.");
            //     //}
            // }
            // catch (Exception ex)
            // {
            //     TempData["markNewError"] = ex.Message;

            //     return RedirectToAction("Index", new { id = emailId });
            // }

            //return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkClosed(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkClosedStatus(emailId, userId);
            }
            catch (Exception ex)
            {
                TempData["markNewError"] = ex.Message;

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

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