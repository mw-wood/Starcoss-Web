using Microsoft.AspNetCore.Mvc;
using Starcross.Data;  // Ensure you're using your AppDbContext namespace
using Starcross.Models;  // Ensure you're using your Models namespace
using System.Diagnostics;

namespace Starcross.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor injection for DbContext
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Ensure the model is properly initialized with RequestId
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            // Pass the model to the view
            return View(model);
        }
    }
}
