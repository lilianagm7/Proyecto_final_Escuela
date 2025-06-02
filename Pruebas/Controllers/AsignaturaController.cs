using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pruebas.Models;
using System.Data;

namespace Pruebas.Controllers
{
    public class AsignaturaController : Controller
    {
        private readonly EscuelaBContext _context;

        public AsignaturaController(EscuelaBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var asignaturas = await _context.Asignaturas
                .FromSqlRaw("EXEC SP_Consultar_Asignatura")
                .ToListAsync();
            return View(asignaturas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            var asignatura = await _context.Asignaturas
                .FromSqlRaw("EXEC SP_Consultar_Asignatura_ID @id", param)
                .FirstOrDefaultAsync();

            if (asignatura == null) return NotFound();
            return View(asignatura);
        }

        public IActionResult Create()
        {
            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Nombre");
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Asignatura asignatura)
        {
            if (ModelState.IsValid)
            {
                var parametros = new[]
                {
                    new SqlParameter("@nombre", SqlDbType.VarChar) { Value = asignatura.Nombre },
                    new SqlParameter("@creditos", SqlDbType.Float) { Value = asignatura.Creditos },
                    new SqlParameter("@tipo", SqlDbType.VarChar) { Value = asignatura.Tipo },
                    new SqlParameter("@curso", SqlDbType.TinyInt) { Value = asignatura.Curso },
                    new SqlParameter("@cuatrimestre", SqlDbType.TinyInt) { Value = asignatura.Cuatrimestre },
                    new SqlParameter("@id_profesor", SqlDbType.Int) { Value = asignatura.IdProfesor },
                    new SqlParameter("@id_grado", SqlDbType.Int) { Value = asignatura.IdGrado }
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC SP_Insertar_Asignatura @nombre, @creditos, @tipo, @curso, @cuatrimestre, @id_profesor, @id_grado", parametros);
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Nombre", asignatura.IdGrado);
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Nombre", asignatura.IdProfesor);
            return View(asignatura);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            var asignatura = _context.Asignaturas
                .FromSqlRaw("EXEC SP_Consultar_Asignatura_ID @id", param)
                .AsEnumerable()
                .FirstOrDefault();

            if (asignatura == null) return NotFound();

            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Nombre", asignatura.IdGrado);
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Nombre", asignatura.IdProfesor);
            return View(asignatura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Asignatura asignatura)
        {
            if (id != asignatura.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var parametros = new[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = asignatura.Id },
                    new SqlParameter("@nombre", SqlDbType.VarChar) { Value = asignatura.Nombre },
                    new SqlParameter("@creditos", SqlDbType.Float) { Value = asignatura.Creditos },
                    new SqlParameter("@tipo", SqlDbType.VarChar) { Value = asignatura.Tipo },
                    new SqlParameter("@curso", SqlDbType.TinyInt) { Value = asignatura.Curso },
                    new SqlParameter("@cuatrimestre", SqlDbType.TinyInt) { Value = asignatura.Cuatrimestre },
                    new SqlParameter("@id_profesor", SqlDbType.Int) { Value = asignatura.IdProfesor },
                    new SqlParameter("@id_grado", SqlDbType.Int) { Value = asignatura.IdGrado }
                };

                await _context.Database.ExecuteSqlRawAsync("EXEC SP_Actualizar_Asignatura @id, @nombre, @creditos, @tipo, @curso, @cuatrimestre, @id_profesor, @id_grado", parametros);
                return RedirectToAction(nameof(Index));
            }

            ViewData["IdGrado"] = new SelectList(_context.Grados, "Id", "Nombre", asignatura.IdGrado);
            ViewData["IdProfesor"] = new SelectList(_context.Profesors, "Id", "Nombre", asignatura.IdProfesor);
            return View(asignatura);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            var asignatura = await _context.Asignaturas
                .FromSqlRaw("EXEC SP_Consultar_Asignatura_ID @id", param)
                .FirstOrDefaultAsync();

            if (asignatura == null) return NotFound();
            return View(asignatura);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_Eliminar_Asignatura @id", param);
            return RedirectToAction(nameof(Index));
        }
    }
}
