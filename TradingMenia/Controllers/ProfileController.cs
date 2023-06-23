using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;


namespace TradingMenia.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string requestToken)
        {
            string apiUrl = "https://api.kite.trade/user/profile";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");

                // Prepare the request parameters
                var requestContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("api_key", apiKey),
                    new KeyValuePair<string, string>("request_token", requestToken)
                });

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, requestContent);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    string userProfile = await response.Content.ReadAsStringAsync();
                    ViewData["UserProfile"] = userProfile;
                }
                else
                {
                    ViewData["Error"] = "Failed to retrieve user profile. Status code: " + response.StatusCode;
                }
            }
            return View();
        }
    }
}