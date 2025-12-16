using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UTB.BaChr.Mapy.Infrastructure.Database;
using UTB.BaChr.Mapy.Domain.Entities;
using System.Linq;

namespace UTB.BaChr.Mapy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly MapyDbContext _context;

        public UsersController(MapyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(User model)
        {
            // Načteme uživatele z DB, abychom měli původní heslo
            var userFromDb = _context.Users.Find(model.Id);
            if (userFromDb == null) return NotFound();

            // Aktualizujeme pouze povolená pole
            userFromDb.Name = model.Name;
            userFromDb.Email = model.Email;
            userFromDb.Role = model.Role;

            // HESLO A PASSWORDHASH NEAKTUALIZUJEME - Zůstává původní

            _context.Users.Update(userFromDb);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Delete (volitelné, ale v adminu běžné)
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}