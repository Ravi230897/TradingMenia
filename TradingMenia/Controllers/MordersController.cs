using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;


namespace TradingMenia.Controllers
{
    public class MordersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken, string orderId, string orderType, int quantity, string validity)
        {
            string apiUrl = $"https://api.kite.trade/orders/regular/{orderId}";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");
                client.DefaultRequestHeaders.Add("Authorization", $"token {apiKey}:{accessToken}");

                // Prepare the request data
                var requestData = new
                {
                    order_type = orderType,
                    quantity = quantity,
                    validity = validity
                };

                // Send the PUT request with data
                HttpResponseMessage response = await client.PutAsJsonAsync(apiUrl, requestData);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Order modified successfully.";
                }
                else
                {
                    ViewData["Error"] = "Failed to modify the order. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}
