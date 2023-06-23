using Microsoft.AspNetCore.Mvc;

namespace TradingMenia.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string url)
        {
            // Redirect to the login page
            return Redirect(url);
        }
    }
}