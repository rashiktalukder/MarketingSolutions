using MarketingSolutions.DataAccess;
using MarketingSolutions.Helper;
using MarketingSolutions.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketingSolutions.Controllers
{
    public class CompaniesController : BaseController
    {
        private readonly ApplicationDbContext dbContext;

        public CompaniesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyList()
        {
            var companyList = await dbContext.Companies.ToListAsync();

            return Json(new { success = true, Message = $"Company list got Successfully!", values = companyList });
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company companyObj)
        {
            if(string.IsNullOrEmpty(companyObj.Name))
            {
                return Json(new { Success = false, Message = "Company Name is Empty." });
            }
            var isExist = await dbContext.Companies.AnyAsync(x=>x.Name.ToLower() == companyObj.Name.ToLower() && CommonHelper.GetUserId(HttpContext) == x.UserId);
            if (isExist)
            {
                return Json(new { Success = false, Message = "This Company Name is Already Exists.<br/> Please check the list below." });
            }

            companyObj.UserId = CommonHelper.GetUserId(HttpContext);

            await dbContext.AddAsync(companyObj);
            await dbContext.SaveChangesAsync();
            return Json(new { Success = true, Message = $"{companyObj.Name} Added Successfully!" });
        }

        [HttpPut]
        public async Task<IActionResult> Update(Company company)
        {
            if(company.Id == Guid.Empty)
            {
                return Json(new { success = false, message = "Company Not Found!" });
            }

            company.UserId = CommonHelper.GetUserId(HttpContext);

            dbContext.Companies.Update(company);
            await dbContext.SaveChangesAsync();

            return Json(new { success = true, message = "Company Updated Successfully!" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string companyId)
        {
            var company = await dbContext.Companies.FindAsync(Guid.Parse(companyId));
            if (company == null)
            {
                return Json(new { success = false, message = "Company Not Found!" });
            }

            dbContext.Companies.Remove(company);
            dbContext.SaveChanges();

            return Json(new { success = true, message = "Company Deleted Successfully!" });
        }
    }
}
