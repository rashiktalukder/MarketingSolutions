using MarketingSolutions.DataAccess;
using MarketingSolutions.Models;
using MarketingSolutions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

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

        [HttpPost]
        public async Task<ActionResult> UserRegistration(RegisterViewModel RegistrationObj)
        {

            // Check if passwords match
            if (RegistrationObj.Password != RegistrationObj.ConfirmPassword)
            {
                return Json(new { success = false, errors = new { Message = "Passwords do not match." } });
            }

            if(dbContext.Users.Any(u=>u.Email == RegistrationObj.Email))
            {
                return Json(new { success = false, errors = new { Message = "Email Already Exists." } });
            }

            User user = new User
            {
                Name = RegistrationObj.UserName,
                Email = RegistrationObj.Email,
                PasswordHash = HashPassword(RegistrationObj.Password)
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Json(new { success = true, Message = $"{RegistrationObj.UserName} Registerd Successfully!" });
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }
}
