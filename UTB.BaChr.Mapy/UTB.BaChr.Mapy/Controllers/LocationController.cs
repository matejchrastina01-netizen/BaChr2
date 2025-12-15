using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;

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
            // Metodu GetByIdWithDetails musíte mít implementovanou v ILocationService/LocationService (z kroku 4)
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
            if (!string.IsNullOrWhiteSpace(text))
            {
                // Získáme ID přihlášeného uživatele
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdStr, out int userId))
                {
                    var comment = new Comment
                    {
                        LocationId = locationId,
                        UserId = userId,
                        Text = text,
                        DateCreated = DateTime.Now
                    };

                    _locationService.AddComment(comment);
                }
            }
            return RedirectToAction("Details", new { id = locationId });
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
        public async Task<IActionResult> UploadPhoto(int locationId, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Cesta do složky wwwroot/uploads
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Vytvoření unikátního názvu souboru
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Uložení souboru na disk
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Uložení záznamu do DB
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdStr, out int userId))
                {
                    var photo = new Photo
                    {
                        LocationId = locationId,
                        UserId = userId,
                        ImagePath = "/uploads/" + uniqueFileName
                    };

                    _locationService.AddPhoto(photo);
                }
            }

            return RedirectToAction("Details", new { id = locationId });
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