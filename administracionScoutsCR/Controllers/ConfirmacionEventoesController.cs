using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using administracionScoutsCR.Models;

namespace administracionScoutsCR.Controllers
{
    public class ConfirmacionEventoesController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public ConfirmacionEventoesController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: ConfirmacionEventoes
        public async Task<IActionResult> Index()
        {
            var databaseScoutContext = _context.ConfirmacionEventos.Include(c => c.IdEventoNavigation).Include(c => c.IdUsuarioNavigation);
            return View(await databaseScoutContext.ToListAsync());
        }

        // GET: ConfirmacionEventoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmacionEvento = await _context.ConfirmacionEventos
                .Include(c => c.IdEventoNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdConfirmacionEvento == id);
            if (confirmacionEvento == null)
            {
                return NotFound();
            }

            return View(confirmacionEvento);
        }

        // GET: ConfirmacionEventoes/Create
        public IActionResult Create()
        {
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: ConfirmacionEventoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdConfirmacionEvento,Asistencia,IdEvento,IdUsuario")] ConfirmacionEvento confirmacionEvento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(confirmacionEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento", confirmacionEvento.IdEvento);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", confirmacionEvento.IdUsuario);
            return View(confirmacionEvento);
        }

        // GET: ConfirmacionEventoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmacionEvento = await _context.ConfirmacionEventos.FindAsync(id);
            if (confirmacionEvento == null)
            {
                return NotFound();
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento", confirmacionEvento.IdEvento);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", confirmacionEvento.IdUsuario);
            return View(confirmacionEvento);
        }

        // POST: ConfirmacionEventoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdConfirmacionEvento,Asistencia,IdEvento,IdUsuario")] ConfirmacionEvento confirmacionEvento)
        {
            if (id != confirmacionEvento.IdConfirmacionEvento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(confirmacionEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfirmacionEventoExists(confirmacionEvento.IdConfirmacionEvento))
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
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "IdEvento", confirmacionEvento.IdEvento);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", confirmacionEvento.IdUsuario);
            return View(confirmacionEvento);
        }

        // GET: ConfirmacionEventoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var confirmacionEvento = await _context.ConfirmacionEventos
                .Include(c => c.IdEventoNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdConfirmacionEvento == id);
            if (confirmacionEvento == null)
            {
                return NotFound();
            }

            return View(confirmacionEvento);
        }

        // POST: ConfirmacionEventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var confirmacionEvento = await _context.ConfirmacionEventos.FindAsync(id);
            if (confirmacionEvento != null)
            {
                _context.ConfirmacionEventos.Remove(confirmacionEvento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfirmacionEventoExists(int id)
        {
            return _context.ConfirmacionEventos.Any(e => e.IdConfirmacionEvento == id);
        }
    }
}
