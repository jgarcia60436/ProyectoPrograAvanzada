using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class UbicacionesController : Controller
    {
        private readonly BibliotecaContext _context;

        public UbicacionesController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Ubicaciones
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Ubicaciones.Include(u => u.Libro);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Ubicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ubicaciones == null)
            {
                return NotFound();
            }

            var ubicacione = await _context.Ubicaciones
                .Include(u => u.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacione == null)
            {
                return NotFound();
            }

            return View(ubicacione);
        }

        // GET: Ubicaciones/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id");
            return View();
        }

        // POST: Ubicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LibroId,Estante,Seccion,CodigoUbicacion")] Ubicacione ubicacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ubicacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", ubicacione.LibroId);
            return View(ubicacione);
        }

        // GET: Ubicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ubicaciones == null)
            {
                return NotFound();
            }

            var ubicacione = await _context.Ubicaciones.FindAsync(id);
            if (ubicacione == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", ubicacione.LibroId);
            return View(ubicacione);
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,Estante,Seccion,CodigoUbicacion")] Ubicacione ubicacione)
        {
            if (id != ubicacione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ubicacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UbicacioneExists(ubicacione.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", ubicacione.LibroId);
            return View(ubicacione);
        }

        // GET: Ubicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ubicaciones == null)
            {
                return NotFound();
            }

            var ubicacione = await _context.Ubicaciones
                .Include(u => u.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ubicacione == null)
            {
                return NotFound();
            }

            return View(ubicacione);
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ubicaciones == null)
            {
                return Problem("Entity set 'BibliotecaContext.Ubicaciones'  is null.");
            }
            var ubicacione = await _context.Ubicaciones.FindAsync(id);
            if (ubicacione != null)
            {
                _context.Ubicaciones.Remove(ubicacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UbicacioneExists(int id)
        {
          return (_context.Ubicaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
