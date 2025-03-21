using MarketingSolutions.Helper;
using MarketingSolutions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MarketingSolutions.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*if(!CommonHelper.IsUserAuthenticated(HttpContext))
            {
                return RedirectToAction("Login","Account");
            }*/

            // Get the username from the session
            var username = CommonHelper.GetUsername(HttpContext);
            ViewBag.Username = username;

            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
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
