using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewHotel.Data;
using NewHotel.Models;
using System.Security.Claims;

namespace NewHotel.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public LoginController(ApplicationDbContext db, IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _db = db;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Utente utente)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _db.Utente.FirstOrDefaultAsync(u => u.Username == utente.Username && u.Password == utente.Password);

            if (user == null)
            {
                TempData["error"] = "Account non esistente";
                return View();
            }

            var claims = new List<Claim>
         {
             new Claim(ClaimTypes.Name, user.Username),
         };



            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            TempData["success"] = "Login effettuato con successo";

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            TempData["success"] = "Sei stato disconnesso";

            return RedirectToAction("Index", "Home");
        }


    }
}
