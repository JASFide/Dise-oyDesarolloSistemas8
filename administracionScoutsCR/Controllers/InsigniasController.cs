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
    public class InsigniasController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public InsigniasController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: Insignias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insignias.ToListAsync());
        }

        // GET: Insignias/Details/5
        // GET: Insignias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insignia = await _context.Insignias
                .Include(i => i.ReqInsignia) // Make sure this is included
                .FirstOrDefaultAsync(m => m.IdInsignia == id);

            if (insignia == null)
            {
                return NotFound();
            }

            return View(insignia);
        }

        // GET: Insignias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insignias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsignia,Nombre,Seccion,Tipo,Estado")] Insignia insignia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insignia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insignia);
        }

        // GET: Insignias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insignia = await _context.Insignias.FindAsync(id);
            if (insignia == null)
            {
                return NotFound();
            }
            return View(insignia);
        }

        // POST: Insignias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInsignia,Nombre,Seccion,Tipo,Estado")] Insignia insignia)
        {
            if (id != insignia.IdInsignia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insignia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsigniaExists(insignia.IdInsignia))
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
            return View(insignia);
        }

        // GET: Insignias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insignia = await _context.Insignias
                .FirstOrDefaultAsync(m => m.IdInsignia == id);
            if (insignia == null)
            {
                return NotFound();
            }

            return View(insignia);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insignia = await _context.Insignias.FindAsync(id);
            if (insignia != null)
            {
                _context.Insignias.Remove(insignia);
                await _context.SaveChangesAsync();
            }
            return Ok(); // Importante para fetch
        }


        private bool InsigniaExists(int id)
        {
            return _context.Insignias.Any(e => e.IdInsignia == id);
        }
    }
}
