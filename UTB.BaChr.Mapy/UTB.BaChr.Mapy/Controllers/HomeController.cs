using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Models;

namespace UTB.BaChr.Mapy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILocationService _locationService; // Služba pro data

        // Injektáž služby
        public HomeController(ILogger<HomeController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        public IActionResult Index()
        {
            // Získání všech lokací a jejich odeslání do View
            var locations = _locationService.Select();
            return View(locations);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}