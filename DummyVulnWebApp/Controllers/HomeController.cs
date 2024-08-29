using DummyVulnWebApp.Context;
using DummyVulnWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DummyVulnWebApp.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/SaveInput
        [HttpPost]
        public async Task<IActionResult> SaveInput(string userInput)
        {
            // Save the input to the database
            var input = new UserInput { InputValue = userInput };
            _context.UserInputs.Add(input);
            await _context.SaveChangesAsync();

            // Redirect to the DisplayMessage action with the input ID
            return RedirectToAction("DisplayMessage", new { id = input.Id });
        }

        // GET: Home/DisplayMessage
        public async Task<IActionResult> DisplayMessage(int id)
        {
            // Retrieve the input from the database using the ID
            var input = await _context.UserInputs.FindAsync(id);

            if (input == null)
            {
                return NotFound();
            }

            // Display the message in the view
            ViewBag.Message = $"You entered: {input.InputValue}";
            return View();
        }
    }
}
