using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pruebas.Models;

namespace Pruebas.Controllers
{
    public class GradoController : Controller
    {
        private readonly EscuelaBContext _context;

        public GradoController(EscuelaBContext context)
        {
            _context = context;
        }

        // GET: Grado
        public async Task<IActionResult> Index()
        {
              return _context.Grados != null ? 
                          View(await _context.Grados.ToListAsync()) :
                          Problem("Entity set 'EscuelaBContext.Grados'  is null.");
        }

        // GET: Grado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Grados == null)
            {
                return NotFound();
            }

            var grado = await _context.Grados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grado == null)
            {
                return NotFound();
            }

            return View(grado);
        }

        // GET: Grado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Grado grado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grado);
        }

        // GET: Grado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Grados == null)
            {
                return NotFound();
            }

            var grado = await _context.Grados.FindAsync(id);
            if (grado == null)
            {
                return NotFound();
            }
            return View(grado);
        }

        // POST: Grado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Grado grado)
        {
            if (id != grado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradoExists(grado.Id))
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
            return View(grado);
        }

        // GET: Grado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Grados == null)
            {
                return NotFound();
            }

            var grado = await _context.Grados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grado == null)
            {
                return NotFound();
            }

            return View(grado);
        }

        // POST: Grado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Grados == null)
            {
                return Problem("Entity set 'EscuelaBContext.Grados'  is null.");
            }
            var grado = await _context.Grados.FindAsync(id);
            if (grado != null)
            {
                _context.Grados.Remove(grado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradoExists(int id)
        {
          return (_context.Grados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
