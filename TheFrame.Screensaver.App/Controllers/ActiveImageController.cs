using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TheFrame.Screensaver.App.Controllers {
	[Route("api/active-image")]
	[ApiController]
	public class ActiveImageController : ControllerBase {
		private readonly IConfiguration _config;

		public ActiveImageController(IConfiguration configuration) {
			_config = configuration;
		}

		[HttpGet]
		public async Task<string> GetActiveImageUrl(string user, string token) {
			if(IsIPAuthenticated())
				return GetAuthenticatedImageUrl();

			return GetDefaultImageUrl();
		}

		[HttpGet("client-ip")]
		public string GetClientIp() {
			return Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
		}
		
		public bool IsIPAuthenticated() {
			string ip = Request.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
			string allowedIp = _config["ALLOWED_CLIENT_IP"];

			return ip.Equals(allowedIp);
		}

		public string GetDefaultImageUrl() {
			var suffix = "nature1";
			return $"https://raw.githubusercontent.com/ddhatman/theframe-roku-screensaver/master/assets/tv-background-{suffix}.jpg";
		}

		public string GetAuthenticatedImageUrl() {
			var suffix = "nature1";

			var dt = DateTime.UtcNow.AddHours(-7);

			switch(dt.Month) {
				case 1:
				case 2:
					suffix = "wintermountains";
					break;
				case 3:
					if(dt.Day == 9)
						suffix = "birthday";
					else
						suffix = "nature1";
					break;
				case 4:
					suffix = "nature2";
					break;
				case 5:
					if(dt.Day == 6)
						suffix = "birthday";
					else
						suffix = "beach";
					break;
				case 6:
					suffix = "beach";
					break;
				case 7:
					if(dt.Day == 4)
						suffix = "fireworks";
					else if(dt.Day < 20)
						suffix = "beach";
					else
						suffix = "sedona";
					break;
				case 8:
					suffix = "sedona";
					break;
				case 9:
					if(dt.Day < 20)
						suffix = "sedona";
					else
						suffix = "royalpalms";
					break;
				case 10:
					if(dt.Day < 14)
						suffix = "royalpalms";
					else if(dt.Day == 14)
						suffix = "birthday";
					else if(dt.Day == 31)
						suffix = "halloween";
					else
						suffix = "fallpumpkins";
					break;
				case 11:
					if((int)dt.DayOfWeek - dt.Day < 0)
						suffix = "fallpumpkins";
					else
						suffix = "disneyxmas";
					break;
				case 12:
					if(dt.Day < 28)
						suffix = "disneyxmas";
					else if(dt.Day == 31)
						suffix = "fireworks";
					else
						suffix = "wintermountains";
					break;
			}

			return $"https://raw.githubusercontent.com/ddhatman/theframe-roku-screensaver/master/assets/tv-background-{suffix}.jpg";
		}
	}
}
