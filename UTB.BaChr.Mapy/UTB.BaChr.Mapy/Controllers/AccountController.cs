using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTB.BaChr.Mapy.Application.Abstraction;
using UTB.BaChr.Mapy.Domain.Entities;
using UTB.BaChr.Mapy.Infrastructure.Database;
using UTB.BaChr.Mapy.Models;

namespace UTB.BaChr.Mapy.Controllers
{
    public class AccountController : Controller
    {
        private readonly MapyDbContext _dbContext;
        private readonly ISecurityService _securityService;

        public AccountController(MapyDbContext dbContext, ISecurityService securityService)
        {
            _dbContext = dbContext;
            _securityService = securityService;
        }

        // GET: Zobrazení registračního formuláře
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Zpracování registrace
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kontrola existence uživatele
                if (_dbContext.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Uživatel s tímto emailem již existuje.");
                    return View(model);
                }

                var user = new User
                {
                    Email = model.Email,
                    Name = model.Name,
                    Role = "User" // Defaultní role
                };

                // Hashování hesla
                user.PasswordHash = _securityService.HashPassword(user, model.Password);

                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        // GET: Zobrazení přihlašovacího formuláře (TOTO VÁM PRAVDĚPODOBNĚ CHYBĚLO)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Zpracování přihlášení
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == model.Email);

                // Ověření uživatele a hesla pomocí SecurityService
                if (user != null && _securityService.VerifyPassword(user, user.PasswordHash, model.Password))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("FullName", user.Name ?? ""),
                        new Claim(ClaimTypes.Role, user.Role ?? "User"),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Neplatné přihlašovací údaje.");
            }
            return View(model);
        }

        // Odhlášení
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}