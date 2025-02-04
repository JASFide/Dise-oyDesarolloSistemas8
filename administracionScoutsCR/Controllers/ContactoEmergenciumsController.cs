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
    public class ContactoEmergenciumsController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public ContactoEmergenciumsController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: ContactoEmergenciums
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactoEmergencia.ToListAsync());
        }

        // GET: ContactoEmergenciums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactoEmergencium = await _context.ContactoEmergencia
                .FirstOrDefaultAsync(m => m.IdContactoEmergencia == id);
            if (contactoEmergencium == null)
            {
                return NotFound();
            }

            return View(contactoEmergencium);
        }

        // GET: ContactoEmergenciums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactoEmergenciums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdContactoEmergencia,Nombre,Parentesco,NumeroContacto")] ContactoEmergencium contactoEmergencium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactoEmergencium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactoEmergencium);
        }

        // GET: ContactoEmergenciums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactoEmergencium = await _context.ContactoEmergencia.FindAsync(id);
            if (contactoEmergencium == null)
            {
                return NotFound();
            }
            return View(contactoEmergencium);
        }

        // POST: ContactoEmergenciums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdContactoEmergencia,Nombre,Parentesco,NumeroContacto")] ContactoEmergencium contactoEmergencium)
        {
            if (id != contactoEmergencium.IdContactoEmergencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactoEmergencium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoEmergenciumExists(contactoEmergencium.IdContactoEmergencia))
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
            return View(contactoEmergencium);
        }

        // GET: ContactoEmergenciums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactoEmergencium = await _context.ContactoEmergencia
                .FirstOrDefaultAsync(m => m.IdContactoEmergencia == id);
            if (contactoEmergencium == null)
            {
                return NotFound();
            }

            return View(contactoEmergencium);
        }

        // POST: ContactoEmergenciums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactoEmergencium = await _context.ContactoEmergencia.FindAsync(id);
            if (contactoEmergencium != null)
            {
                _context.ContactoEmergencia.Remove(contactoEmergencium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoEmergenciumExists(int id)
        {
            return _context.ContactoEmergencia.Any(e => e.IdContactoEmergencia == id);
        }
    }
}
