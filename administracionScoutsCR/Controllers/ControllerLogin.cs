using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace administracionScoutsCR.Controllers
{
    public class ControllerLogin : Controller
    {
        private readonly DatabaseScoutContext _context;
        public ControllerLogin(DatabaseScoutContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Usuarios.ToList());

      
        }
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(LoginViewmodel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _context.Usuarios
                    .Where(x => x.Correo == model.Correo && x.Contrasena == model.Contrasena)
                    .FirstOrDefault();

                if (usuario != null)
                {
                    var rol = _context.Role.FirstOrDefault(r => r.Id == usuario.IdRole);
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Correo),
                new Claim("Name", usuario.Nombre),
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, rol != null ? rol.Nombre : string.Empty)
            };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    ModelState.AddModelError("", "Usuario o contraseña no coinciden");
                }
            }
            return View();
        }



        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); 
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}
