using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailManager.Data;
using EmailManager.Data.Implementation;
using EmailManager.Mappers;
using EmailManager.Models.EmailViewModel;
using EmailManager.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailManager.Controllers
{
    public class EmailController : Controller
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IEmailService _emailService;

        public EmailController(IEmailService service)
        {
            this._emailService = service;
        }

        public IActionResult Detail(int id)
        {
            var email = _emailService.GetEmail(id);
            var emailAttachments = _emailService.GetAttachment(id);

            var emailModel = new EmailViewModel(email, emailAttachments);

            log.Info($"User opened email detail page. Email Id: {id}");

            return View("Detail", emailModel);
        }

        public IActionResult Index(/*int? currPage*/)
        {
            //var currentPage = currPage ?? 1;

            //int totalPages = await _emailService.GetPageCount(5);
            //IEnumerable<Email> emailsPP = await _emailService.GetAllStatusEmails(currentPage);

            //var emailsListing = emailsPP
            //    .Select(m => EmailMapper.MapFromEmail(m, _emailService));

            //var model = new EmailIndexViewModel()
            //{
            //    CurrentPage = currentPage,
            //    TotalPages = totalPages,
            //    Emails = emailsListing,
            //};

            //if (totalPages > currentPage)
            //{
            //    model.NextPage = currentPage + 1;
            //}

            //if (currentPage > 1)
            //{
            //    model.PreviewPage = currentPage - 1;
            //}

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListAllStatusEmails(int? currPage, string search = null)
        {
            var currentPage = currPage ?? 1;

            int totalPages = await _emailService.GetPageCount(10);

            IEnumerable<Email> emailAllResults = null;

            if (!string.IsNullOrEmpty(search))
            {
                emailAllResults = await _emailService.SearchEmails(search, currentPage);
                log.Info($"User searched for {search}.");
            }
            else
            {
                emailAllResults = await _emailService.GetAllStatusEmails(currentPage);
                log.Info($"Displayed all emails list.");
            }

            var emailsListing = emailAllResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing, currentPage, totalPages);

            if (totalPages > currentPage)
            {
                emailModel.NextPage = currentPage + 1;
            }

            if (currentPage > 1)
            {
                emailModel.PreviewPage = currentPage - 1;
            }

            return View(emailModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkNew(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = id;

            try
            {
                await _emailService.MarkNewStatus(emailId, userId);
                log.Info($"User changed email status to new. User Id: {userId} Email Id: {id}");

                //if (markNew == false)
                //{
                //    throw new ArgumentException("You cannot change the status because it is already changed.");
                //}
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                log.Info($"System failed to change the email status to new. User Id: {userId} Email Id: {id}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkClosedApproved(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkClosedApprovedStatus(emailId, userId);
                log.Info($"User changed email status to closed - approved. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                log.Error($"System failed to change the email status to closed - approved. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkClosedRejected(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkClosedRejectedStatus(emailId, userId);
                log.Info($"User changed email status to closed - rejected. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                log.Error($"System failed to change the email status to closed - rejected. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkInvalid(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkInvalidStatus(emailId, userId);
                log.Info($"User changed email status to invalid. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                log.Error($"System failed to change the email status to invalid. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkOpen(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkOpenStatus(emailId, userId);
                log.Info($"User changed email status to open. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                log.Error($"System failed to change the email status to open. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> MarkNotReviewed(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkNotReviewStatus(emailId, userId);
                log.Info($"User changed email status to not reviewed. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                log.Error($"System failed to change the email status to not reviewed. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }
    }
}