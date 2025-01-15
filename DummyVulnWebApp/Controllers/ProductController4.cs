using DummyVulnWebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DummyVulnWebApp.Controllers
{
    public class ProductController4 : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController4(ApplicationDbContext context)
        {
            _context = context;
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }
            return true;
        }

        public async Task<IActionResult> Index(string evening)
        {
            var query = $"Select * From Products where Evening = " + evening;

            var evenings = await _context.Products
                                .FromSqlRaw(query, evening)
                                .ToListAsync();

            return View(evenings);
        }
    }
}
