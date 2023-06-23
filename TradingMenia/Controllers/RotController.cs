using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class RotController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken, string orderId)
        {
            string apiUrl = $"https://api.kite.trade/orders/{orderId}/trades";

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
                    string responseData = await response.Content.ReadAsStringAsync();

                    // Process and use the order trades data as per your requirement

                    ViewData["Message"] = "Order trades retrieved successfully.";
                }
                else
                {
                    ViewData["Error"] = "Failed to retrieve order trades. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}