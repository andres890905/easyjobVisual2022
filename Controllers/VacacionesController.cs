using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easyjob22.Models;

namespace easyjob22.Controllers
{
    public class VacacionesController : Controller
    {
        private readonly EasyjobContext _context;

        public VacacionesController(EasyjobContext context)
        {
            _context = context;
        }

        // GET: Vacaciones
        public async Task<IActionResult> Index()
        {
            var easyjobContext = _context.Vacaciones.Include(v => v.IdusuariosNavigation);
            return View(await easyjobContext.ToListAsync());
        }

        // GET: Vacaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacacione = await _context.Vacaciones
                .Include(v => v.IdusuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdVacacion == id);
            if (vacacione == null)
            {
                return NotFound();
            }

            return View(vacacione);
        }

        // GET: Vacaciones/Create
        public IActionResult Create()
        {
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios");
            return View();
        }

        // POST: Vacaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVacacion,Idusuarios,FechaInicio,FechaFin,Estado,FechaSolicitud,Comentarios")] Vacacione vacacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vacacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", vacacione.Idusuarios);
            return View(vacacione);
        }

        // GET: Vacaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacacione = await _context.Vacaciones.FindAsync(id);
            if (vacacione == null)
            {
                return NotFound();
            }
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", vacacione.Idusuarios);
            return View(vacacione);
        }

        // POST: Vacaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVacacion,Idusuarios,FechaInicio,FechaFin,Estado,FechaSolicitud,Comentarios")] Vacacione vacacione)
        {
            if (id != vacacione.IdVacacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacacioneExists(vacacione.IdVacacion))
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
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", vacacione.Idusuarios);
            return View(vacacione);
        }

        // GET: Vacaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vacacione = await _context.Vacaciones
                .Include(v => v.IdusuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdVacacion == id);
            if (vacacione == null)
            {
                return NotFound();
            }

            return View(vacacione);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vacacione = await _context.Vacaciones.FindAsync(id);
            if (vacacione != null)
            {
                _context.Vacaciones.Remove(vacacione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacacioneExists(int id)
        {
            return _context.Vacaciones.Any(e => e.IdVacacion == id);
        }
    }
}
