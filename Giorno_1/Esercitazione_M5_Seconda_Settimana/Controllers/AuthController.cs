using Esercitazione_M5_Seconda_Settimana.Models;
using Esercitazione_M5_Seconda_Settimana.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Esercitazione_M5_Seconda_Settimana.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users u)
        {
            try
            {
                var user = _authService.Login(u.Username, u.Password);
                if (user == null)
                {
                    return RedirectToAction("Register");
                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, u.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            }
            catch (Exception ex) { }
            return RedirectToAction("GetAllPrenotazioni", "Prenotazioni");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Register(Users u)
        {
            try
            {
                var user = _authService.Register(u.Username, u.Password);
            }
            catch (Exception ex) { }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
