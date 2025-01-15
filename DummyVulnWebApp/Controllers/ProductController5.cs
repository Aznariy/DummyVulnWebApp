using DummyVulnWebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DummyVulnWebApp.Controllers
{
    public class ProductController5 : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController5(ApplicationDbContext context)
        {
            _context = context;
        }

        public static string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public async Task<IActionResult> Index(string unit)
        {
            var query = $"Select * From Products where Category = " + unit;

            var units = await _context.Products
                                .FromSqlRaw(query, unit)
                                .ToListAsync();

            return View(units);
        }
    }
}
