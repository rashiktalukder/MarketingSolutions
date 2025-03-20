using MarketingSolutions.DataAccess;
using MarketingSolutions.Models;
using MarketingSolutions.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UserLogin(LoginViewModel LoginObj)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Email == LoginObj.Email);
            if(user == null)
            {
                return Json(new { success = false, errors = new { Message = "User Not Found. Email Does not Exist!" }});
            }
            var hashedLoginPass = HashPassword(LoginObj.Password);
            if(user.PasswordHash != hashedLoginPass)
            {
                return Json(new { success = false, errors = new { Message = "Incorrect Password!" } });
            }

            // Store user ID and Name in session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Name);

            var xxx = HttpContext.Session.GetString("Username");

            return Json(new { success = true, Message = $"{user.Name} Login Successfully!" });

        }

        public ActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
