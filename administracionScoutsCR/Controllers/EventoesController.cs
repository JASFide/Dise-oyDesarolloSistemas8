using System;
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

namespace administracionScoutsCR.Controllers
{
    public class EventoesController : Controller
    {
        private readonly DatabaseScoutContext _context;
        private readonly IConfiguration _configuration;

       /* public EventoesController(DatabaseScoutContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }*/

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
            var usuarioId = ObtenerUsuarioId();

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

        private int ObtenerUsuarioId()
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value);
        }
    }
}
