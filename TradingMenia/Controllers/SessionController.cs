using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string requestToken, string checksum)
        {
            string apiUrl = "https://api.kite.trade/session/token";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");

                // Prepare the request parameters
                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("api_key", apiKey),
                    new KeyValuePair<string, string>("request_token", requestToken),
                    new KeyValuePair<string, string>("checksum", checksum)
                });

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, requestContent);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    string sessionToken = await response.Content.ReadAsStringAsync();
                    ViewData["SessionToken"] = sessionToken;
                }
                else
                {
                    ViewData["Error"] = "Failed to generate session token. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}