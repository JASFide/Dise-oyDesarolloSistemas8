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
    public class UsuariosController : Controller
    {
        private readonly DatabaseScoutContext _context;

        public UsuariosController(DatabaseScoutContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var databaseScoutContext = _context.Usuarios.Include(u => u.IdSeccionNavigation).Include(u => u.IdRoleNavigation);

            return View(await databaseScoutContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdSeccionNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()

        {

            ViewBag.SeccionesDisponibles = new SelectList(_context.Seccions, "IdSeccion", "Nombre");

            ViewBag.Role = new SelectList(_context.Role, "Id", "Nombre");

            return View();

        }


        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellido1,Apellido2,FechaNacimiento,TipoUsuario,Estado,IdSeccion,Direccion,Correo,NumeroTelefono,Contrasena,IdRole")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == usuario.Correo);
                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Correo", "El correo ya está en uso.");
                }
                else
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["IdSeccion"] = new SelectList(_context.Seccions, "IdSeccion", "Nombre", usuario.IdSeccion);
            ViewBag.Roles = new SelectList(_context.Role, "Id", "Nombre", usuario.IdRole);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdSeccion"] = new SelectList(_context.Seccions, "IdSeccion", "IdSeccion", usuario.IdSeccion);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellido1,Apellido2,FechaNacimiento,TipoUsuario,Estado,IdSeccion,Direccion,Correo,NumeroTelefono,Contrasena")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
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
            ViewData["IdSeccion"] = new SelectList(_context.Seccions, "IdSeccion", "IdSeccion", usuario.IdSeccion);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdSeccionNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
