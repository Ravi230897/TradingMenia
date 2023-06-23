using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class MarginsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken)
        {
            string apiUrl = "https://api.kite.trade/user/margins";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");
                client.DefaultRequestHeaders.Add("Authorization", $"token {apiKey}:{accessToken}");

                // Send the GET request
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    string margins = await response.Content.ReadAsStringAsync();
                    ViewData["Margins"] = margins;
                }
                else
                {
                    ViewData["Error"] = "Failed to retrieve margins. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}