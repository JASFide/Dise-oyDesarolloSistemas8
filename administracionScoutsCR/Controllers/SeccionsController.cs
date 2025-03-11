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
    public class SeccionsController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public SeccionsController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: Seccions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Seccions.ToListAsync());
        }

        // GET: Seccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccions
                .FirstOrDefaultAsync(m => m.IdSeccion == id);
            if (seccion == null)
            {
                return NotFound();
            }

            return View(seccion);
        }

        // GET: Seccions/Create
        public IActionResult Create()
        {
            ViewBag.Encargados = new SelectList(
                _context.Usuarios.Where(u => u.TipoUsuario == "Facilitador").ToList(),
                "IdUsuario",
                "Nombre"
            );

            return View();
        }

        // POST: Seccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSeccion,Nombre,EdadMinima,EdadMaxima,JefeSeccion")] Seccion seccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seccion);
        }


        // GET: Seccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccions.FindAsync(id);
            if (seccion == null)
            {
                return NotFound();
            }
            return View(seccion);
        }

        // POST: Seccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSeccion,Nombre,EdadMinima,EdadMaxima,JefeSeccion")] Seccion seccion)
        {
            if (id != seccion.IdSeccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeccionExists(seccion.IdSeccion))
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
            return View(seccion);
        }

        // GET: Seccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seccion = await _context.Seccions
                .FirstOrDefaultAsync(m => m.IdSeccion == id);
            if (seccion == null)
            {
                return NotFound();
            }

            return View(seccion);
        }

        // POST: Seccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seccion = await _context.Seccions.FindAsync(id);
            if (seccion != null)
            {
                _context.Seccions.Remove(seccion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeccionExists(int id)
        {
            return _context.Seccions.Any(e => e.IdSeccion == id);
        }
    }
}
