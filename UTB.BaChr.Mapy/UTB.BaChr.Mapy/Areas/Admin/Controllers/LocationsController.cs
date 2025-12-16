using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UTB.BaChr.Mapy.Infrastructure.Database;
using UTB.BaChr.Mapy.Domain.Entities;
using System.Linq;

namespace UTB.BaChr.Mapy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private readonly MapyDbContext _context;

        public LocationsController(MapyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Locations.ToList());
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public IActionResult Create(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Add(location);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var location = _context.Locations.Find(id);
            if (location == null) return NotFound();
            return View(location);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Update(location);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        public IActionResult Delete(int id)
        {
            var location = _context.Locations.Find(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}