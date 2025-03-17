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
    public class ReqInsigniasController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public ReqInsigniasController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: ReqInsignias
        public async Task<IActionResult> Index()
        {
            var databaseScoutContext = _context.ReqInsignia.Include(r => r.IdInsigniasNavigation);
            return View(await databaseScoutContext.ToListAsync());
        }

        // GET: ReqInsignias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reqInsignia = await _context.ReqInsignia
                .Include(r => r.IdInsigniasNavigation)
                .FirstOrDefaultAsync(m => m.IdReqInsignia == id);
            if (reqInsignia == null)
            {
                return NotFound();
            }

            return View(reqInsignia);
        }

        // GET: ReqInsignias/Create
        public IActionResult Create()
        {
            ViewBag.InsigniasDisponibles = new SelectList(_context.Insignias, "IdInsignia", "Nombre");

            return View();
        }

        // POST: ReqInsignias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdReqInsignia,Descripcion,IdInsignias")] ReqInsignia reqInsignia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reqInsignia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInsignias"] = new SelectList(_context.Insignias, "IdInsignia", "IdInsignia", reqInsignia.IdInsignias);
            return View(reqInsignia);
        }

        // GET: ReqInsignias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reqInsignia = await _context.ReqInsignia.FindAsync(id);
            if (reqInsignia == null)
            {
                return NotFound();
            }
            ViewData["IdInsignias"] = new SelectList(_context.Insignias, "IdInsignia", "IdInsignia", reqInsignia.IdInsignias);
            return View(reqInsignia);
        }

        // POST: ReqInsignias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdReqInsignia,Descripcion,IdInsignias")] ReqInsignia reqInsignia)
        {
            if (id != reqInsignia.IdReqInsignia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reqInsignia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReqInsigniaExists(reqInsignia.IdReqInsignia))
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
            ViewData["IdInsignias"] = new SelectList(_context.Insignias, "IdInsignia", "IdInsignia", reqInsignia.IdInsignias);
            return View(reqInsignia);
        }

        // GET: ReqInsignias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reqInsignia = await _context.ReqInsignia
                .Include(r => r.IdInsigniasNavigation)
                .FirstOrDefaultAsync(m => m.IdReqInsignia == id);
            if (reqInsignia == null)
            {
                return NotFound();
            }

            return View(reqInsignia);
        }

        // POST: ReqInsignias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reqInsignia = await _context.ReqInsignia.FindAsync(id);
            if (reqInsignia != null)
            {
                _context.ReqInsignia.Remove(reqInsignia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReqInsigniaExists(int id)
        {
            return _context.ReqInsignia.Any(e => e.IdReqInsignia == id);
        }
    }
}
