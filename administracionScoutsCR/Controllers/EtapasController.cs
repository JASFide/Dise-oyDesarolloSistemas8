using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace administracionScoutsCR.Controllers
{
    public class EtapasController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public EtapasController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: Etapas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Etapas.ToListAsync());
        }

        // GET: Etapas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapa = await _context.Etapas.FirstOrDefaultAsync(m => m.IdEtapa == id);
            if (etapa == null)
            {
                return NotFound();
            }

            return View(etapa);
        }

        // GET: Etapas/Create
        [Authorize(Roles = "Facilitador,Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etapas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEtapa,Nombre,Seccion,Estado")] Etapa etapa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etapa);
        }

        // GET: Etapas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapa = await _context.Etapas.FindAsync(id);
            if (etapa == null)
            {
                return NotFound();
            }
            return View(etapa);
        }

        // POST: Etapas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEtapa,Nombre,Seccion,Estado")] Etapa etapa)
        {
            if (id != etapa.IdEtapa)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapaExists(etapa.IdEtapa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(etapa);
        }

        // GET: Etapas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var etapa = await _context.Etapas.FirstOrDefaultAsync(m => m.IdEtapa == id);
            if (etapa == null)
            {
                return NotFound();
            }

            return View(etapa);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etapa = await _context.Etapas.FindAsync(id);
            if (etapa != null)
            {
                _context.Etapas.Remove(etapa);
                await _context.SaveChangesAsync();
            }
            return Ok(); // para que funcione bien con fetch
        }


        private bool EtapaExists(int id)
        {
            return _context.Etapas.Any(e => e.IdEtapa == id);
        }

        // GET: Etapas/Asignar
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

        // POST: Etapas/Asignar
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
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "El usuario ya tiene asignada esta etapa.");
            }

            ViewBag.Usuarios = new SelectList(_context.Usuarios, "IdUsuario", "Nombre", asignacion.IdUsuario);
            ViewBag.Etapas = new SelectList(_context.Etapas, "IdEtapa", "Nombre", asignacion.IdEtapa);
            return View(asignacion);
        }
    }
}