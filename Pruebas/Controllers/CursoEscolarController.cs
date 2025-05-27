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
    public class CursoEscolarController : Controller
    {
        private readonly EscuelaBContext _context;

        public CursoEscolarController(EscuelaBContext context)
        {
            _context = context;
        }

        // GET: CursoEscolar
        public async Task<IActionResult> Index()
        {
              return _context.CursoEscolars != null ? 
                          View(await _context.CursoEscolars.ToListAsync()) :
                          Problem("Entity set 'EscuelaBContext.CursoEscolars'  is null.");
        }

        // GET: CursoEscolar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CursoEscolars == null)
            {
                return NotFound();
            }

            var cursoEscolar = await _context.CursoEscolars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoEscolar == null)
            {
                return NotFound();
            }

            return View(cursoEscolar);
        }

        // GET: CursoEscolar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CursoEscolar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnyoInicio,AnyoFin")] CursoEscolar cursoEscolar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cursoEscolar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cursoEscolar);
        }

        // GET: CursoEscolar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CursoEscolars == null)
            {
                return NotFound();
            }

            var cursoEscolar = await _context.CursoEscolars.FindAsync(id);
            if (cursoEscolar == null)
            {
                return NotFound();
            }
            return View(cursoEscolar);
        }

        // POST: CursoEscolar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnyoInicio,AnyoFin")] CursoEscolar cursoEscolar)
        {
            if (id != cursoEscolar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cursoEscolar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CursoEscolarExists(cursoEscolar.Id))
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
            return View(cursoEscolar);
        }

        // GET: CursoEscolar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CursoEscolars == null)
            {
                return NotFound();
            }

            var cursoEscolar = await _context.CursoEscolars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cursoEscolar == null)
            {
                return NotFound();
            }

            return View(cursoEscolar);
        }

        // POST: CursoEscolar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CursoEscolars == null)
            {
                return Problem("Entity set 'EscuelaBContext.CursoEscolars'  is null.");
            }
            var cursoEscolar = await _context.CursoEscolars.FindAsync(id);
            if (cursoEscolar != null)
            {
                _context.CursoEscolars.Remove(cursoEscolar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CursoEscolarExists(int id)
        {
          return (_context.CursoEscolars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
