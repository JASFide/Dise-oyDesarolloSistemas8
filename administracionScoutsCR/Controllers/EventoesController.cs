﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        // GET: Eventoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.ConfirmacionEventos)
                .ThenInclude(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(e => e.IdEvento == id);

            if (evento == null)
            {
                return NotFound();
            }
            var confirmedUsers = evento.ConfirmacionEventos
                .Where(c => c.Asistencia.Trim().ToLower() == "confirmado") // Asegura que coincida con el texto correcto
                .Select(c => c.IdUsuarioNavigation)
                .ToList();

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
        public async Task<IActionResult> Create(Evento evento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (evento.ImagenEvento != null && evento.ImagenEvento.Length > 0)
                    {
                        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "eventos");

                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(evento.ImagenEvento.FileName)}";
                        var filePath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await evento.ImagenEvento.CopyToAsync(stream);
                        }

                        evento.RutaImagen = $"/uploads/eventos/{fileName}";
                    }

                    _context.Add(evento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                return View(evento);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al guardar el evento: " + ex.Message);
                return View(evento);
            }
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
					await EnviarCorreoRecordatorio(correo, evento);
				}
			}

			return Ok("Correos enviados para eventos próximos.");
		}

		private async Task EnviarCorreoRecordatorio(string destinatario, Evento evento)
		{
			var apiKey = _configuration["SendGrid:ApiKey"];
			var fromEmail = _configuration["SendGrid:FromEmail"];

			var client = new SendGridClient(apiKey);
			var from = new EmailAddress(fromEmail, "ScoutsCR");
			var subject = $"Recordatorio: {evento.Titulo} en 7 días";
			var to = new EmailAddress(destinatario);
			var plainTextContent = $"Hola, te recordamos que el evento '{evento.Titulo}' se llevará a cabo el {evento.Fecha}. ¡No faltes!";
			var htmlContent = $"<strong>Hola,</strong><br><br>Te recordamos que el evento <b>{evento.Titulo}</b> se llevará a cabo el <b>{evento.Fecha}</b>.<br>¡No faltes!";
			var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

			var response = await client.SendEmailAsync(msg);
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

        private int ObtenerUsuarioId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value);
        }
    }
}
