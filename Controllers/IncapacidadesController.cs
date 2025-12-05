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
    public class IncapacidadesController : Controller
    {
        private readonly EasyjobContext _context;

        public IncapacidadesController(EasyjobContext context)
        {
            _context = context;
        }

        // GET: Incapacidades
        public async Task<IActionResult> Index()
        {
            var easyjobContext = _context.Incapacidades.Include(i => i.IdusuariosNavigation);
            return View(await easyjobContext.ToListAsync());
        }

        // GET: Incapacidades/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incapacidade = await _context.Incapacidades
                .Include(i => i.IdusuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdIncapacidad == id);
            if (incapacidade == null)
            {
                return NotFound();
            }

            return View(incapacidade);
        }

        // GET: Incapacidades/Create
        public IActionResult Create()
        {
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios");
            return View();
        }

        // POST: Incapacidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdIncapacidad,Idusuarios,NombreEmpleado,NombreEps,Motivo,FechaInicio,FechaFin,ArchivoSoporte")] Incapacidade incapacidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incapacidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", incapacidade.Idusuarios);
            return View(incapacidade);
        }

        // GET: Incapacidades/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incapacidade = await _context.Incapacidades.FindAsync(id);
            if (incapacidade == null)
            {
                return NotFound();
            }
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", incapacidade.Idusuarios);
            return View(incapacidade);
        }

        // POST: Incapacidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdIncapacidad,Idusuarios,NombreEmpleado,NombreEps,Motivo,FechaInicio,FechaFin,ArchivoSoporte")] Incapacidade incapacidade)
        {
            if (id != incapacidade.IdIncapacidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incapacidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncapacidadeExists(incapacidade.IdIncapacidad))
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
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", incapacidade.Idusuarios);
            return View(incapacidade);
        }

        // GET: Incapacidades/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incapacidade = await _context.Incapacidades
                .Include(i => i.IdusuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdIncapacidad == id);
            if (incapacidade == null)
            {
                return NotFound();
            }

            return View(incapacidade);
        }

        // POST: Incapacidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var incapacidade = await _context.Incapacidades.FindAsync(id);
            if (incapacidade != null)
            {
                _context.Incapacidades.Remove(incapacidade);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncapacidadeExists(string id)
        {
            return _context.Incapacidades.Any(e => e.IdIncapacidad == id);
        }
    }
}
