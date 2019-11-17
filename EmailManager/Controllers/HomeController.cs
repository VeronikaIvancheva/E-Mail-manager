using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailManager.Models;
using EmailManager.Services.Contracts;
using System.Net.Http;
using EmailManager.Data.DTO;

namespace EmailManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly IGmailAPIService _emailService;

		public HomeController(IGmailAPIService emailService)
		{
			this._emailService = emailService;
		}

		public async Task<IActionResult> Index()
		{			
			await _emailService.SaveEmailsToDB();

			return View();
		}
		
		public async Task<bool> ReadGmail(HttpClient client, GmailCredentialsDTO userData)
		{
			client.DefaultRequestHeaders.Add("Authorization", "Bearer " + userData.AccessToken);

			var res = await client.GetAsync("https://www.googleapis.com/gmail/v1/users/me/messages");
			//var content = await res.Content.ReadAsStringAsync();

			return res.IsSuccessStatusCode;
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
