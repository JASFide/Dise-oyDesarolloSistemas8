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
		[AllowAnonymous]
		public IActionResult login()
        {
            return View();
        }
        [HttpPost]
		[AllowAnonymous]
	    public async Task<IActionResult> Login(LoginViewmodel model)
    {
        if (ModelState.IsValid)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Correo == model.Correo);

            if (usuario != null)
            {
                var hasher = new PasswordHasher<Usuario>();
                var result = hasher.VerifyHashedPassword(usuario, usuario.Contrasena, model.Contrasena);

                if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
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

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Panel", "Home");
                }
            }

            ModelState.AddModelError("", "Usuario o contraseña no coinciden");
        }

        return View(model);
    }



		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "ControllerLogin");
		}

		[Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
    }
}

