using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TradingMenia.Controllers
{
    public class HDController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string apiKey, string accessToken, int instrumentId, DateTime from, DateTime to)
        {
            string apiUrl = $"https://api.kite.trade/instruments/historical/{instrumentId}/minute?from={from:yyyy-MM-dd HH:mm:ss}&to={to:yyyy-MM-dd HH:mm:ss}";

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

                    // Process and use the historical data as per your requirement

                    ViewData["Message"] = "Historical data retrieved successfully.";
                }
                else
                {
                    ViewData["Error"] = "Failed to retrieve historical data. Status code: " + response.StatusCode;
                }
            }

            return View();
        }
    }
}