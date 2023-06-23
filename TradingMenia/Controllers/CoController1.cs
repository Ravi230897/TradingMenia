using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class CoController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken, string orderId)
        {
            string apiUrl = $"https://api.kite.trade/orders/regular/{orderId}";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");
                client.DefaultRequestHeaders.Add("Authorization", $"token {apiKey}:{accessToken}");

                // Send the DELETE request
                HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Order cancelled successfully.";
                }
                else
                {
                    ViewData["Error"] = "Failed to cancel the order. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}