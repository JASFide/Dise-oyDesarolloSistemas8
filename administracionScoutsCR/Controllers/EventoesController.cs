using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using administracionScoutsCR.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using static administracionScoutsCR.Models.Evento;
using System.Diagnostics.Metrics;

namespace administracionScoutsCR.Controllers
{
    public class EventoesController : Controller
	{
		private readonly DatabaseScoutContext _context;
		private readonly IConfiguration _configuration;

        public EventoesController(DatabaseScoutContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            int? usuarioId = ObtenerUsuarioId();

            var eventos = await _context.Eventos
                .Include(e => e.ConfirmacionEventos)
                .ToListAsync();

            // Contar confirmaciones por evento
            ViewBag.Confirmaciones = eventos.ToDictionary(e => e.IdEvento, e => e.ConfirmacionEventos.Count);

            // Verificar si el usuario ha confirmado su asistencia
            ViewBag.UsuarioConfirmaciones = usuarioId == null
                ? new Dictionary<int, bool>()
                : eventos.ToDictionary(
                    e => e.IdEvento,
                    e => e.ConfirmacionEventos.Any(c => c.IdUsuario == usuarioId.Value)
                );

            return View(eventos);
        }
        public async Task<IActionResult> Calendar()
        {
            return View(await _context.Eventos.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .FirstOrDefaultAsync(e => e.IdEvento == id);

            if (evento == null)
            {
                return NotFound();
            }

            // Obtener la lista de usuarios que confirmaron asistencia
            var confirmedUsers = await _context.ConfirmacionEventos
                .Include(c => c.IdUsuarioNavigation)
                .Where(c => c.IdEvento == id)
                .Select(c => c.IdUsuarioNavigation)
                .ToListAsync();

            ViewBag.ConfirmedUsers = confirmedUsers;

            return View(evento);
        }


        // GET: Eventoes/Create
        // GET: Eventoes1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eventoes1/Create
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

        // Método para verificar eventos próximos en 7 días
        public async Task<IActionResult> VerificarEventosProximos()
        {
            var fechaLimite = DateTime.Now.AddDays(7);
            var eventosProximos = await _context.Eventos
                .Where(e => e.Fecha.Date == fechaLimite.Date)
                .ToListAsync();

            foreach (var evento in eventosProximos)
            {
                var usuariosConfirmados = await _context.ConfirmacionEventos
                    .Where(c => c.IdEvento == evento.IdEvento)
                    .Select(c => c.IdUsuarioNavigation.Correo)
                    .ToListAsync();

                foreach (var correo in usuariosConfirmados)
                {
                 //   await EnviarCorreoRecordatorio(correo, evento);
                }
            }

            return Ok("Correos enviados para eventos próximos.");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarAsistencia(int idEvento)
        {
            int? usuarioId = ObtenerUsuarioId();
            if (usuarioId == null)
            {
                return Json(new { success = false, message = "Usuario no autenticado." });
            }

            var confirmacion = new ConfirmacionEvento
            {
                IdEvento = idEvento,
                IdUsuario = usuarioId.Value,
                Asistencia = "Confirmado"
            };

            _context.ConfirmacionEventos.Add(confirmacion);
            await _context.SaveChangesAsync();

            int totalConfirmados = _context.ConfirmacionEventos.Count(c => c.IdEvento == idEvento);
            return Json(new { success = true, confirmado = true, totalConfirmados });
        }

        [HttpPost]
        public async Task<IActionResult> EliminarConfirmacion(int idEvento)
        {
            int? usuarioId = ObtenerUsuarioId();
            if (usuarioId == null)
            {
                return Json(new { success = false, message = "Usuario no autenticado." });
            }

            var confirmacion = await _context.ConfirmacionEventos
                .FirstOrDefaultAsync(c => c.IdEvento == idEvento && c.IdUsuario == usuarioId);

            if (confirmacion != null)
            {
                _context.ConfirmacionEventos.Remove(confirmacion);
                await _context.SaveChangesAsync();
            }

            int totalConfirmados = _context.ConfirmacionEventos.Count(c => c.IdEvento == idEvento);
            return Json(new { success = true, confirmado = false, totalConfirmados });
        }

        private int? ObtenerUsuarioId()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "IdUsuario");

            if (claim != null && int.TryParse(claim.Value, out int userId))
            {
                return userId;
            }

            // Opcional: log o manejo personalizado
            return null; // O lanzar excepción si es requerido
        }
    }
}