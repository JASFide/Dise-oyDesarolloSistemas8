// UsuarioInsigniaController.cs
using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace administracionScoutsCR.Controllers
{
    public class UsuarioInsigniaController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public UsuarioInsigniaController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: UsuarioInsignia/Asignar
        public IActionResult Asignar(int? idInsignia = null)
        {
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            ViewBag.Insignias = new SelectList(_context.Insignias, "IdInsignia", "Nombre", idInsignia);

            var modelo = new UsuarioxInsignium();
            if (idInsignia.HasValue)
            {
                modelo.IdInsignia = idInsignia.Value;
            }

            return View(modelo);
        }

        // POST: UsuarioInsignia/Asignar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Asignar([Bind("IdUsuario,IdInsignia")] UsuarioxInsignium asignacion)
        {
            if (ModelState.IsValid)
            {
                var yaExiste = await _context.UsuarioxInsignia
                    .AnyAsync(u => u.IdUsuario == asignacion.IdUsuario && u.IdInsignia == asignacion.IdInsignia);

                if (!yaExiste)
                {
                    _context.Add(asignacion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Insignias"); // O redirige a Details si lo preferís
                }

                ModelState.AddModelError("", "El usuario ya tiene asignada esta insignia.");
            }
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    if (entry.Value?.Errors.Count > 0)
                    {
                        Console.WriteLine($"Error en propiedad: {entry.Key}");
                        foreach (var error in entry.Value.Errors)
                        {
                            Console.WriteLine($" - {error.ErrorMessage}");
                        }
                    }
                }
            }


            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", asignacion.IdUsuario);
            ViewData["IdInsignia"] = new SelectList(_context.Insignias, "IdInsignia", "Nombre", asignacion.IdInsignia);

            return View(asignacion);
        }


    }
}
