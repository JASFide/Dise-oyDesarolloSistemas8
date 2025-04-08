using administracionScoutsCR.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using administracionScoutsCR.Models;
using Microsoft.EntityFrameworkCore;


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

            var insignias = await _context.Insignias
                .Where(i => i.Estado.ToLower() == "en progreso")
                .ToListAsync(); // Aqu� deber�as filtrar por usuario si hay una relaci�n

            var etapas = await _context.Etapas
                .Where(e => e.Seccion == usuario.IdSeccionNavigation.Nombre)
                .ToListAsync(); // Igual, ajust� seg�n la relaci�n real con el usuario

            ViewBag.Usuario = usuario;
            ViewBag.Eventos = eventosConfirmados;
            ViewBag.Insignias = insignias;
            ViewBag.Etapas = etapas;

            return View();
        }
    }
}
