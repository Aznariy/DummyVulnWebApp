using DummyVulnWebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DummyVulnWebApp.Controllers
{
    public class ProductController2 : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController2(ApplicationDbContext context)
        {
            _context = context;
        }

        public static int CalculateFactorial(int n)
        {
            if (n <= 1)
                return 1;
            return n * CalculateFactorial(n - 1);
        }

        public async Task<IActionResult> Index(string strategy)
        {
            var query = $"Select * From Products where Strategy = " + strategy;

            var strategies = await _context.Products
                                .FromSqlRaw(query, strategy)
                                .ToListAsync();

            return View(strategies);
        }
    }
}
