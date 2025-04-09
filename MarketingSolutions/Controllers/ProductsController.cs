using Microsoft.AspNetCore.Mvc;

namespace MarketingSolutions.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
