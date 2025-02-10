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
    public class EventoesController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public EventoesController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: Eventoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Eventos.ToListAsync());
        }
        // GET: Eventoes
        public async Task<IActionResult> Calendar()
        {
            return View(await _context.Eventos.ToListAsync());
        }
        [HttpGet]
        public JsonResult GetEventos()
        {
            var eventos = _context.Eventos.Select(e => new
            {
                title = e.Titulo,
                start = e.Fecha.ToString("yyyy-MM-dd"),
                url = Url.Action("Details", "Eventoes", new { id = e.IdEvento })
            }).ToList();

            return Json(eventos);
        }


        // GET: Eventoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEvento,Titulo,Fecha,Lugar,Descripcion,Encargado,ContactoEncargado")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evento);
        }

        // GET: Eventoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            return View(evento);
        }

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEvento,Titulo,Fecha,Lugar,Descripcion,Encargado,ContactoEncargado")] Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.IdEvento))
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
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(m => m.IdEvento == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento != null)
            {
                _context.Eventos.Remove(evento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(int id)
        {
            return _context.Eventos.Any(e => e.IdEvento == id);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmarAsistencia(int idEvento)
        {
            TempData["Mensaje"] = "Ya has confirmado tu asistencia a este evento.";
            var usuarioId = ObtenerUsuarioId(); // Implementa cómo obtienes el ID del usuario autenticado

            // Verificar si ya existe una confirmación
            var confirmacionExistente = await _context.ConfirmacionEventos
                .FirstOrDefaultAsync(c => c.IdEvento == idEvento && c.IdUsuario == usuarioId);

            if (confirmacionExistente == null)
            {
                var confirmacion = new ConfirmacionEvento
                {
                    IdEvento = idEvento,
                    IdUsuario = usuarioId,
                    Asistencia = "Confirmado"
                };

                _context.ConfirmacionEventos.Add(confirmacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // Método para obtener el ID del usuario autenticado (debes adaptarlo a tu autenticación)
        private int ObtenerUsuarioId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value);
        }




    }
}
