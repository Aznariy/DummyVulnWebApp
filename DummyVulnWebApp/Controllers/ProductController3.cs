using DummyVulnWebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DummyVulnWebApp.Controllers
{
    public class ProductController3 : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController3(ApplicationDbContext context)
        {
            _context = context;
        }

        public static long CalculateFibonacci(int n)
        {
            if (n <= 1)
                return n;

            long prev = 0, current = 1;

            for (int i = 2; i <= n; i++)
            {
                long next = prev + current;
                prev = current;
                current = next;
            }
            return current;
        }

        public async Task<IActionResult> Index(string morning)
        {
            var query = $"Select * From Products where Morning = " + morning;

            var mornings = await _context.Products
                                .FromSqlRaw(query, morning)
                                .ToListAsync();

            return View(mornings);
        }
    }
}
