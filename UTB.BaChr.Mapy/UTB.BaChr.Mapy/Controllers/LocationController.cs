using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Models;

namespace UTB.BaChr.Mapy.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LocationController(ILocationService locationService, IWebHostEnvironment hostEnvironment)
        {
            _locationService = locationService;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Zobrazení detailu lokace
        public IActionResult Details(int id)
        {
            // Metodu GetByIdWithDetails musíme mít implementovanou v ILocationService/LocationService
            var location = _locationService.GetByIdWithDetails(id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Přidání komentáře
        [HttpPost]
        [Authorize]
        public IActionResult AddComment(int locationId, string text)
        {
            // Manuální vytvoření objektu pro validaci
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int.TryParse(userIdStr, out int userId);

            var comment = new Comment
            {
                LocationId = locationId,
                UserId = userId,
                Text = text ?? "", // Ošetření null pro validaci
                DateCreated = DateTime.Now
            };

            // Validace modelu (Entity)
            if (TryValidateModel(comment))
            {
                _locationService.AddComment(comment);
                // Úspěch -> přesměrovat na GET (Pattern PRG)
                return RedirectToAction("Details", new { id = locationId });
            }

            // Validace selhala -> Musíme znovu načíst stránku Details a zobrazit chyby
            var location = _locationService.GetByIdWithDetails(locationId);
            if (location == null) return NotFound();

            // Vrátíme view Details, ale s chybami v ModelState
            return View("Details", location);
        }

        // POST: Smazání komentáře
        [HttpPost]
        [Authorize]
        public IActionResult DeleteComment(int id, int locationId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdStr, out int userId))
            {
                var isAdmin = User.IsInRole("Admin");
                _locationService.DeleteComment(id, userId, isAdmin);
            }

            return RedirectToAction("Details", new { id = locationId });
        }

        // POST: Nahrání fotky
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(PhotoUploadViewModel model)
        {
            // Kontrola validace (včetně našeho FileContent atributu)
            if (ModelState.IsValid)
            {
                if (model.File != null && model.File.Length > 0)
                {
                    // Cesta do složky wwwroot/uploads
                    var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    int.TryParse(userIdStr, out int userId);

                    var photo = new Photo
                    {
                        LocationId = model.LocationId,
                        UserId = userId,
                        ImagePath = "/uploads/" + uniqueFileName
                    };

                    _locationService.AddPhoto(photo);
                    return RedirectToAction("Details", new { id = model.LocationId });
                }
            }

            // Validace selhala -> Znovu načíst View a zobrazit chyby
            var location = _locationService.GetByIdWithDetails(model.LocationId);
            if (location == null) return NotFound();

            return View("Details", location);
        }

        // POST: Smazání fotky
        [HttpPost]
        [Authorize]
        public IActionResult DeletePhoto(int id, int locationId)
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdStr, out int userId))
            {
                var isAdmin = User.IsInRole("Admin");
                _locationService.DeletePhoto(id, userId, isAdmin);
            }

            return RedirectToAction("Details", new { id = locationId });
        }
    }
}