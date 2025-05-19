using System.Diagnostics;
using EmployeeTimeSheet.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeSheet.Controllers
{
    // I delete the "Home" folder from views, since the intent here was not to have a company landing page. 
    // I wanted to remove any additional links or routing that were generated when I created the project in Visual Studio
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // To ensure any links or pages that were pre-built into the solution didn't point to a non-existent homepage, I routed to TimeSheet
        public IActionResult Index()
        {
            return RedirectToAction("Index", "TimeSheet");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
