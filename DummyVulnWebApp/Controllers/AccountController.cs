using DummyVulnWebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace DummyVulnWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var passwordHash = HashPassword(password);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == passwordHash);

            if (user == null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("Username", user.Username);

                return RedirectToAction("Index", "Homr");
            }

            ViewBag.Message = "Invalid username or password";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(password);
                password = Convert.ToBase64String(plainTextBytes);
            }
            return password;
        }
    }
}
