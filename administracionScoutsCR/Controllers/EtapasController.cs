using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using administracionScoutsCR.Models;
using static System.Collections.Specialized.BitVector32;

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

            var etapa = await _context.Etapas
                .FirstOrDefaultAsync(m => m.IdEtapa == id);
            if (etapa == null)
            {
                return NotFound();
            }

            return View(etapa);
        }


        // GET: Etapas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etapas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var etapa = await _context.Etapas
                .FirstOrDefaultAsync(m => m.IdEtapa == id);
            if (etapa == null)
            {
                return NotFound();
            }

            return View(etapa);
        }

        // POST: Etapas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var etapa = await _context.Etapas.FindAsync(id);
            if (etapa != null)
            {
                _context.Etapas.Remove(etapa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapaExists(int id)
        {
            return _context.Etapas.Any(e => e.IdEtapa == id);
        }
    }
}
