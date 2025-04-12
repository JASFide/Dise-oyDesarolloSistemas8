using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace administracionScoutsCR.Controllers
{
    public class UsuarioEtapaController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public UsuarioEtapaController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: UsuarioEtapa/Asignar
        public IActionResult Asignar(int? idEtapa = null)
        {
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            ViewBag.Etapas = new SelectList(_context.Etapas, "IdEtapa", "Nombre", idEtapa);

            var modelo = new UsuarioxEtapa();
            if (idEtapa.HasValue)
            {
                modelo.IdEtapa = idEtapa.Value;
            }

            return View(modelo);
        }

        // POST: UsuarioEtapa/Asignar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Asignar([Bind("IdUsuario,IdEtapa")] UsuarioxEtapa asignacion)
        {
            if (ModelState.IsValid)
            {
                var yaExiste = await _context.UsuarioxEtapas
                    .AnyAsync(u => u.IdUsuario == asignacion.IdUsuario && u.IdEtapa == asignacion.IdEtapa);

                if (!yaExiste)
                {
                    _context.Add(asignacion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Etapas", new { id = asignacion.IdEtapa });
                }

                ModelState.AddModelError("", "El usuario ya tiene asignada esta etapa.");
            }

            ViewBag.Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", asignacion.IdUsuario);
            ViewBag.Etapas = new SelectList(_context.Etapas, "IdEtapa", "Nombre", asignacion.IdEtapa);
            return View(asignacion);
        }
    }
}
