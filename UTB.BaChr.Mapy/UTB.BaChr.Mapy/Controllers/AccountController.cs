using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database;
using UTB.BaChr.Mapy.Models;

namespace UTB.BaChr.Mapy.Controllers
{
    public class AccountController : Controller
    {
        private readonly MapyDbContext _context;

        public AccountController(MapyDbContext context)
        {
            _context = context;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Tento email už je registrován.");
                    return View(model);
                }

                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    // POZNÁMKA: V reálné aplikaci zde heslo zahashujte!
                    PasswordHash = model.Password,
                    Role = "User"
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.PasswordHash == model.Password);

                if (user != null)
                {
                    var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.Email),
    new Claim("FullName", user.Name ?? ""),
    new Claim(ClaimTypes.Role, user.Role ?? "Client"), // Defaultní role je Client (nebo User)
    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // PŘIDÁNO: ID uživatele pro vazbu na data
};

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Špatné jméno nebo heslo.");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}