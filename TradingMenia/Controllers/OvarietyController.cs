using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class OvarietyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken, string tradingSymbol, string exchange, string transactionType, string orderType, int quantity, string product, string validity)
        {
            string apiUrl = "https://api.kite.trade/orders/regular";

            // Prepare the HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Set the required headers
                client.DefaultRequestHeaders.Add("X-Kite-Version", "3");
                client.DefaultRequestHeaders.Add("Authorization", $"token {apiKey}:{accessToken}");

                // Prepare the request data
                var requestData = new
                {
                    tradingsymbol = tradingSymbol,
                    exchange = exchange,
                    transaction_type = transactionType,
                    order_type = orderType,
                    quantity = quantity,
                    product = product,
                    validity = validity
                };

                // Send the POST request with data
                HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, requestData);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    ViewData["Message"] = "Order placed successfully.";
                }
                else
                {
                    ViewData["Error"] = "Failed to place the order. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}