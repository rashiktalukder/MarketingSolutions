using MarketingSolutions.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace MarketingSolutions.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AccountController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
