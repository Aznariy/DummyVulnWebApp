using DummyVulnWebApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DummyVulnWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string category)
        {
            var query = $"Select * From Products where Category = " + category;

            var products = await _context.Products
                                .FromSqlRaw(query, category)
                                .ToListAsync();

            return View(products);
        }
    }
}
