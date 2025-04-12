using MarketingSolutions.DataAccess;
using MarketingSolutions.Helper;
using MarketingSolutions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketingSolutions.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ProductsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index(string companyId)
        {
            ViewBag.CompanyId = companyId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product productObj)
        {
            productObj.UserId = CommonHelper.GetUserId(HttpContext);

            await dbContext.Products.AddAsync(productObj);
            await dbContext.SaveChangesAsync();
            return Json(new { Success = true, Message = $"{productObj.Name} Added Successfully!" });
        }
    }
}
