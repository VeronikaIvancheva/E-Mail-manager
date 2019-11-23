using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmailManager.Models;
using EmailManager.Services.Contracts;

namespace EmailManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGmailAPIService _emailService;

		public HomeController(IGmailAPIService emailService)
		{
			this._emailService = emailService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
