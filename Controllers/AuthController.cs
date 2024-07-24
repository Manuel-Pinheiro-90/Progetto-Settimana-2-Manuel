using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Progetto_Settimana_2_Manuel.Models;
using Progetto_Settimana_2_Manuel.Service;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Progetto_Settimana_2_Manuel.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            this._authService = authService;
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(Utente user)
        {
            {
                try
                {
                    var u = _authService.Login(user.Username, user.PasswordHash);
                    if (u == null) return RedirectToAction("Index", "Home");

                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, u.Username),
                    };
                    u.Ruolo.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r)));
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                }
                catch (Exception ex)
                { }
                return RedirectToAction("Index", "Home");
            }



           



        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult Register()
        {
            return View(); 
               }

        [HttpPost]
        public IActionResult Register(Utente utente)
        {
            _authService.Register(utente);

      return RedirectToAction(); }






    }

    






}
