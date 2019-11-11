using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmailManager.Models;
using EmailManager.Services.Contracts;
using EmailManager.Data.Contracts;
using System.Net.Http;
using Newtonsoft.Json;
using EmailManager.Data.DTO;
using EmailManager.Services.Implementation;
using EmailManager.Data.Implementation;
using System.Text;

namespace EmailManager.Controllers
{
	public class HomeController : Controller
	{
		private readonly IEmailService _emailService;
		private readonly ICredentialsService _credentials;
		private readonly IStatus _status;

		public HomeController(IEmailService emailService, ICredentialsService credentials, IStatus status)
		{
			this._emailService = emailService;
			this._credentials = credentials;
			this._status = status;
		}

		public async Task<IActionResult> Index()
		{
			if (!_credentials.AccessTokenExist().GetAwaiter().GetResult())
			{
				return RedirectToAction("GoogleLogin");
			}

			GoogleCallback(code);
			//ViewBag.emailsCounter = _status;

			//Това е за друго място или горното трябва да се сложи в проверка
			await _emailService.SaveEmailsToDB();

			return View();
		}

		public IActionResult GoogleLogin(string code)
		{
			//	.Append("&redirect_uri=http://localhost:44368/google-callback")

			var sb = new StringBuilder()
				.Append("https://accounts.google.com/o/oauth2/v2/auth?")
				.Append("scope=https://www.googleapis.com/auth/gmail.readonly")
				.Append("&access_type=offline")
				.Append("&include_granted_scopes=true")
				.Append("&state=state_parameter_passthrough_value")
				.Append("&redirect_uri=http://localhost")
				.Append("&response_type=code")
				.Append("&client_id=897371171627-m26anv0387iitqf4drru7hgpqrpskd5v.apps.googleusercontent.com");

			return Redirect(sb.ToString());
		}

		[Route("google-callback")]
		public async Task<IActionResult> GoogleCallback(string code)
		{
			var client = new HttpClient();
			var content = new FormUrlEncodedContent(new[]
			{
				new KeyValuePair<string, string>("code", code),
				new KeyValuePair<string, string>("client_id","897371171627-m26anv0387iitqf4drru7hgpqrpskd5v.apps.googleusercontent.com"),
				new KeyValuePair<string,string>("client_secret","uBbyN8K7xgyVy-nFTxQP4W4a"),
				new KeyValuePair<string,string>("redirect_uri","http://localhost"),
				new KeyValuePair<string,string>("grant_type","authorization_code")
			});

			var res = await client.PostAsync("https://oauth2.googleapis.com/token", content);

			if (!res.IsSuccessStatusCode)
			{
				return Json(await res.Content.ReadAsStringAsync());
			}
			else
			{
				var userDataDTO = JsonConvert.DeserializeObject<GmailCredentialsDTO>
					(await res.Content.ReadAsStringAsync());

				var gmailCredentials = new GmailToken
				{
					AccessToken = userDataDTO.AccessToken,
					RefreshToken = userDataDTO.RefreshToken,
					ExpiredDate = DateTime.Now.AddSeconds(userDataDTO.ExpiresInSeconds)
				};

				await _credentials.AddToken(gmailCredentials);
				return RedirectToAction("Login", "Account");
			}
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
