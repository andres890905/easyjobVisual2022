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
    public class TrasladoesController : Controller
    {
        private readonly EasyjobContext _context;

        public TrasladoesController(EasyjobContext context)
        {
            _context = context;
        }

        // GET: Trasladoes
        public async Task<IActionResult> Index()
        {
            var easyjobContext = _context.Traslados.Include(t => t.IdSucursalDestinoNavigation).Include(t => t.IdSucursalOrigenNavigation).Include(t => t.IdusuariosNavigation);
            return View(await easyjobContext.ToListAsync());
        }

        // GET: Trasladoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = await _context.Traslados
                .Include(t => t.IdSucursalDestinoNavigation)
                .Include(t => t.IdSucursalOrigenNavigation)
                .Include(t => t.IdusuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdTraslados == id);
            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // GET: Trasladoes/Create
        public IActionResult Create()
        {
            ViewData["IdSucursalDestino"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal");
            ViewData["IdSucursalOrigen"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal");
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios");
            return View();
        }

        // POST: Trasladoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTraslados,Idusuarios,IdSucursalOrigen,IdSucursalDestino")] Traslado traslado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(traslado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSucursalDestino"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", traslado.IdSucursalDestino);
            ViewData["IdSucursalOrigen"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", traslado.IdSucursalOrigen);
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", traslado.Idusuarios);
            return View(traslado);
        }

        // GET: Trasladoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = await _context.Traslados.FindAsync(id);
            if (traslado == null)
            {
                return NotFound();
            }
            ViewData["IdSucursalDestino"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", traslado.IdSucursalDestino);
            ViewData["IdSucursalOrigen"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", traslado.IdSucursalOrigen);
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", traslado.Idusuarios);
            return View(traslado);
        }

        // POST: Trasladoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTraslados,Idusuarios,IdSucursalOrigen,IdSucursalDestino")] Traslado traslado)
        {
            if (id != traslado.IdTraslados)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(traslado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrasladoExists(traslado.IdTraslados))
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
            ViewData["IdSucursalDestino"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", traslado.IdSucursalDestino);
            ViewData["IdSucursalOrigen"] = new SelectList(_context.Sucursals, "IdSucursal", "IdSucursal", traslado.IdSucursalOrigen);
            ViewData["Idusuarios"] = new SelectList(_context.Usuarios, "Idusuarios", "Idusuarios", traslado.Idusuarios);
            return View(traslado);
        }

        // GET: Trasladoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var traslado = await _context.Traslados
                .Include(t => t.IdSucursalDestinoNavigation)
                .Include(t => t.IdSucursalOrigenNavigation)
                .Include(t => t.IdusuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdTraslados == id);
            if (traslado == null)
            {
                return NotFound();
            }

            return View(traslado);
        }

        // POST: Trasladoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var traslado = await _context.Traslados.FindAsync(id);
            if (traslado != null)
            {
                _context.Traslados.Remove(traslado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrasladoExists(int id)
        {
            return _context.Traslados.Any(e => e.IdTraslados == id);
        }
    }
}
