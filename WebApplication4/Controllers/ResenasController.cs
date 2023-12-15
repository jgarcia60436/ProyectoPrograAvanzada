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
    public class ResenasController : Controller
    {
        private readonly BibliotecaContext _context;

        public ResenasController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Resenas
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Resenas.Include(r => r.Libro).Include(r => r.Usuario);
            return View(await bibliotecaContext.ToListAsync());
        }

        // GET: Resenas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resenas == null)
            {
                return NotFound();
            }

            var resena = await _context.Resenas
                .Include(r => r.Libro)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resena == null)
            {
                return NotFound();
            }

            return View(resena);
        }

        // GET: Resenas/Create
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Resenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,LibroId,Puntuacion,Comentario,FechaResena")] Resena resena)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", resena.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", resena.UsuarioId);
            return View(resena);
        }

        // GET: Resenas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resenas == null)
            {
                return NotFound();
            }

            var resena = await _context.Resenas.FindAsync(id);
            if (resena == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", resena.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", resena.UsuarioId);
            return View(resena);
        }

        // POST: Resenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,LibroId,Puntuacion,Comentario,FechaResena")] Resena resena)
        {
            if (id != resena.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resena);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResenaExists(resena.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Id", resena.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", resena.UsuarioId);
            return View(resena);
        }

        // GET: Resenas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resenas == null)
            {
                return NotFound();
            }

            var resena = await _context.Resenas
                .Include(r => r.Libro)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resena == null)
            {
                return NotFound();
            }

            return View(resena);
        }

        // POST: Resenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resenas == null)
            {
                return Problem("Entity set 'BibliotecaContext.Resenas'  is null.");
            }
            var resena = await _context.Resenas.FindAsync(id);
            if (resena != null)
            {
                _context.Resenas.Remove(resena);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResenaExists(int id)
        {
          return (_context.Resenas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
