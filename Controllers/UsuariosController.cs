using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easyjob22.Models;
using Microsoft.AspNetCore.Authorization;

namespace easyjob22.Controllers
{[Authorize]
    public class UsuariosController : Controller
    {
        private readonly EasyjobContext _context;

        public UsuariosController(EasyjobContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var easyjobContext = _context.Usuarios.Include(u => u.IdRolesNavigation).Include(u => u.IdSucursalNavigation);
            return View(await easyjobContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolesNavigation)
                .Include(u => u.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.Idusuarios == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdRoles"] = new SelectList(_context.Roles, "IdRoles", "IdRoles");
            ViewData["IdSucursal"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSucursal,IdRoles,Nombre,Apellido,Correo,Contrasena,FechaNacimiento,Telefono,Direccion,Salario")] Usuario usuario)
        {
            // Asignar aquí ANTES de validación
            usuario.FechaRegistro = DateTime.Now;
            usuario.Estado = "Activo";

            // Ahora sí, validar
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("ERROR: " + error.ErrorMessage);
                }

                // Recargar combos
                ViewData["IdRoles"] = new SelectList(_context.Roles, "IdRoles", "TipoRol", usuario.IdRoles);
                ViewData["IdSucursal"] = new SelectList(_context.Sucursals, "IdSucursal", "NombreSucursal", usuario.IdSucursal);

                return View(usuario);
            }

            // Si es válido, guardar
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            ViewData["IdRoles"] = new SelectList(_context.Roles, "IdRoles", "IdRoles", usuario.IdRoles);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", usuario.IdSucursal);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idusuarios,IdSucursal,IdRoles,Nombre,Apellido,Correo,Contrasena,FechaNacimiento,Telefono,Direccion,FechaRegistro,Estado,Salario")] Usuario usuario)
        {
            if (id != usuario.Idusuarios)
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
                    if (!UsuarioExists(usuario.Idusuarios))
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
            ViewData["IdRoles"] = new SelectList(_context.Roles, "IdRoles", "IdRoles", usuario.IdRoles);
            ViewData["IdSucursal"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", usuario.IdSucursal);
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
                .Include(u => u.IdRolesNavigation)
                .Include(u => u.IdSucursalNavigation)
                .FirstOrDefaultAsync(m => m.Idusuarios == id);
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
            return _context.Usuarios.Any(e => e.Idusuarios == id);
        }
    }
}
