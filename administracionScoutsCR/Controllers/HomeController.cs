// HomeController.cs
using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace administracionScoutsCR.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseScoutContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DatabaseScoutContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AccesoDenegado()
        {
            return View();
        }

        public async Task<IActionResult> Panel()
        {
            int? usuarioId = int.TryParse(User.FindFirst("IdUsuario")?.Value, out var id) ? id : (int?)null;
            if (usuarioId == null) return RedirectToAction("Login", "Account");

            var usuario = await _context.Usuarios
                .Include(u => u.IdSeccionNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioId);

            var eventosConfirmados = await _context.ConfirmacionEventos
                .Where(c => c.IdUsuario == usuarioId && c.Asistencia.ToLower() == "confirmado")
                .Include(c => c.IdEventoNavigation)
                .ToListAsync();

            var insignias = await _context.UsuarioxInsignia
                .Where(x => x.IdUsuario == usuarioId && x.Estado.ToLower() == "en progreso")
                .Include(x => x.IdInsigniaNavigation)
                .Select(x => x.IdInsigniaNavigation)
                .ToListAsync();

            var etapas = await _context.UsuarioxEtapas
                .Where(x => x.IdUsuario == usuarioId)
                .Include(x => x.IdEtapaNavigation)
                .Select(x => x.IdEtapaNavigation)
                .ToListAsync();

            ViewBag.Usuario = usuario;
            ViewBag.Eventos = eventosConfirmados;
            ViewBag.Insignias = insignias;
            ViewBag.Etapas = etapas;

            return View();
        }
        public IActionResult Historia()
        {
            return View();
        }

    }
}
