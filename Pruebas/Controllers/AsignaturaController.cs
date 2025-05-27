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
    public class AsignaturaController : Controller
    {
        private readonly EscuelaBContext _context;

        public AsignaturaController(EscuelaBContext context)
        {
            _context = context;
        }

        // GET: Asignatura
        public async Task<IActionResult> Index()
        {
            var escuelaBContext = _context.Asignaturas.Include(a => a.IdGradoNavigation).Include(a => a.IdProfesorNavigation);
            return View(await escuelaBContext.ToListAsync());
        }

        // GET: Asignatura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asignaturas == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignaturas
                .Include(a => a.IdGradoNavigation)
                .Include(a => a.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignatura == null)
            {
                return NotFound();
            }

            return View(asignatura);
        }

        // GET: Asignatura/Create
        public IActionResult Create()
        {
            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Id");
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Id");
            return View();
        }

        // POST: Asignatura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Creditos,Tipo,Curso,Cuatrimestre,IdProfesor,IdGrado")] Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignatura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Id", asignatura.IdGrado);
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Id", asignatura.IdProfesor);
            return View(asignatura);
        }

        // GET: Asignatura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asignaturas == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignaturas.FindAsync(id);
            if (asignatura == null)
            {
                return NotFound();
            }
            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Id", asignatura.IdGrado);
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Id", asignatura.IdProfesor);
            return View(asignatura);
        }

        // POST: Asignatura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Creditos,Tipo,Curso,Cuatrimestre,IdProfesor,IdGrado")] Asignatura asignatura)
        {
            if (id != asignatura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignatura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaExists(asignatura.Id))
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
            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Id", asignatura.IdGrado);
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Id", asignatura.IdProfesor);
            return View(asignatura);
        }

        // GET: Asignatura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asignaturas == null)
            {
                return NotFound();
            }

            var asignatura = await _context.Asignaturas
                .Include(a => a.IdGradoNavigation)
                .Include(a => a.IdProfesorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignatura == null)
            {
                return NotFound();
            }

            return View(asignatura);
        }

        // POST: Asignatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asignaturas == null)
            {
                return Problem("Entity set 'EscuelaBContext.Asignaturas'  is null.");
            }
            var asignatura = await _context.Asignaturas.FindAsync(id);
            if (asignatura != null)
            {
                _context.Asignaturas.Remove(asignatura);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturaExists(int id)
        {
          return (_context.Asignaturas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
