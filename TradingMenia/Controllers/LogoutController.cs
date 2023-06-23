using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken)
        {
            string apiUrl = $"https://api.kite.trade/session/token?api_key={apiKey}&access_token={accessToken}";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");

                // Send the DELETE request
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Session token deleted successfully.";
                }
                else
                {
                    ViewData["Error"] = "Failed to delete session token. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}