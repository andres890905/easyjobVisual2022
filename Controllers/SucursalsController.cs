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
    public class SucursalsController : Controller
    {
        private readonly EasyjobContext _context;

        public SucursalsController(EasyjobContext context)
        {
            _context = context;
        }

        // GET: Sucursals
        public async Task<IActionResult> Index()
        {
            var easyjobContext = _context.Sucursals.Include(s => s.IdZonaNavigation);
            return View(await easyjobContext.ToListAsync());
        }

        // GET: Sucursals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursals
                .Include(s => s.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdSucursal == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // GET: Sucursals/Create
        public IActionResult Create()
        {
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona");
            return View();
        }

        // POST: Sucursals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSucursal,IdZona,NombreSucursal,Direccion,Ciudad,Correo,Telefono")] Sucursal sucursal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", sucursal.IdZona);
            return View(sucursal);
        }

        // GET: Sucursals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursals.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", sucursal.IdZona);
            return View(sucursal);
        }

        // POST: Sucursals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSucursal,IdZona,NombreSucursal,Direccion,Ciudad,Correo,Telefono")] Sucursal sucursal)
        {
            if (id != sucursal.IdSucursal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SucursalExists(sucursal.IdSucursal))
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
            ViewData["IdZona"] = new SelectList(_context.Zonas, "IdZona", "IdZona", sucursal.IdZona);
            return View(sucursal);
        }

        // GET: Sucursals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursals
                .Include(s => s.IdZonaNavigation)
                .FirstOrDefaultAsync(m => m.IdSucursal == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            return View(sucursal);
        }

        // POST: Sucursals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sucursal = await _context.Sucursals.FindAsync(id);
            if (sucursal != null)
            {
                _context.Sucursals.Remove(sucursal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SucursalExists(int id)
        {
            return _context.Sucursals.Any(e => e.IdSucursal == id);
        }
    }
}
