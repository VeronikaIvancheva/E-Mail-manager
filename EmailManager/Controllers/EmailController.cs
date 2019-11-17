using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailManager.Data;
using EmailManager.Mappers;
using EmailManager.Models.EmailViewModel;
using EmailManager.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailManager.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public EmailController(IEmailService service, ILogger<EmailController> logger)
        {
            this._emailService = service;
            this._logger = logger;
        }

        public IActionResult Detail(int id)
        {
            var email = _emailService.GetEmail(id);
            var emailModel = new EmailViewModel(email);

            _logger.LogInformation($"User opened email detail page. Email Id: {id}");

            return View("Detail", emailModel);
        }

        public async Task<IActionResult> Index(string Id)
        {
            var emailsAllResults = await _emailService.GetAllEmails();
            
            var emailsListing = emailsAllResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing);

            _logger.LogInformation("Displayed unfiltered email list.");

            return View(emailModel);
        }

        public async Task<IActionResult> ListAllNewEmails(string Id)
        {
            var juwhgf = Id;
            var emailsAllNewResults = await _emailService.GetAllNewEmails();

            var emailsListing = emailsAllNewResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing);

            _logger.LogInformation("Displayed all new email list.");

            return View(emailModel);
        }

        public async Task<IActionResult> ListAllClosedEmails(string Id)
        {
            var emailsAllNewResults = await _emailService.GetAllClosedEmails();

            var emailsListing = emailsAllNewResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing);

            _logger.LogInformation("Displayed all closed email list.");

            return View(emailModel);
        }

        public async Task<IActionResult> ListAllOpenEmails(string Id)
        {
            var emailsAllNewResults = await _emailService.GetAllOpenedEmails();

            var emailsListing = emailsAllNewResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing);

            _logger.LogInformation("Displayed all open email list.");

            return View(emailModel);
        }

        public async Task<IActionResult> ListAllNotReviewEmails(string Id)
        {
            var emailsAllNewResults = await _emailService.GetAllNotReviewedEmails();

            var emailsListing = emailsAllNewResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing);

            _logger.LogInformation("Displayed all not review email list.");

            return View(emailModel);
        }

        public async Task<IActionResult> ListAllInvalidEmails(string Id)
        {
            var emailsAllNewResults = await _emailService.GetAllNotValidEmails();

            var emailsListing = emailsAllNewResults
                .Select(m => EmailMapper.MapFromEmail(m, _emailService));
            var emailModel = EmailMapper.MapFromEmailIndex(emailsListing);

            _logger.LogInformation("Displayed all invalid email list.");

            return View(emailModel);
        }

        [HttpPost]
        public async Task<IActionResult> MarkNew(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = id;

            try
            {
                await _emailService.MarkNewStatus(emailId, userId);
                _logger.LogInformation($"User changed email status to new. User Id: {userId} Email Id: {id}");

                //if (markNew == false)
                //{
                //    throw new ArgumentException("You cannot change the status because it is already changed.");
                //}
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                _logger.LogError($"User failed to change the email status to new. User Id: {userId} Email Id: {id}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkClosed(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkClosedStatus(emailId, userId);
                _logger.LogInformation($"User changed email status to closed. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                _logger.LogError($"User failed to change the email status to closed. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkInvalid(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkInvalidStatus(emailId, userId);
                _logger.LogInformation($"User changed email status to invalid. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                _logger.LogError($"User failed to change the email status to invalid. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkOpen(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkOpenStatus(emailId, userId);
                _logger.LogInformation($"User changed email status to open. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                _logger.LogError($"User failed to change the email status to open. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }

        [HttpPost]
        public async Task<IActionResult> MarkNotReviewed(EmailViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int emailId = viewModel.Id;

            try
            {
                await _emailService.MarkNotReviewStatus(emailId, userId);
                _logger.LogInformation($"User changed email status to not reviewed. User Id: {userId} Email Id: {emailId}");
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                _logger.LogError($"User failed to change the email status to not reviewed. User Id: {userId} Email Id: {emailId}");

                return RedirectToAction("Index", new { id = emailId });
            }

            return RedirectToAction("Index", new { id = emailId });
        }
    }
}