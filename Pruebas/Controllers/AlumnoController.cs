using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pruebas.Models;
using System.Data;

namespace Pruebas.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly EscuelaBContext _context;

        public AlumnoController(EscuelaBContext context)
        {
            _context = context;
        }

        // GET: Alumno
        public async Task<IActionResult> Index()
        {
            var alumnos = await _context.Alumnos
                .FromSqlRaw("EXEC SP_Consultar_Alumno")
                .ToListAsync();
            return View(alumnos);
        }

        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            var alumno = await _context.Alumnos
                .FromSqlRaw("EXEC SP_Consultar_Alumno_ID @id", param)
                .FirstOrDefaultAsync();

            if (alumno == null) return NotFound();
            return View(alumno);
        }

        // GET: Alumno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var parametros = new[]
                {
                    new SqlParameter("@nif", SqlDbType.VarChar, 9) { Value = alumno.Nif },
                    new SqlParameter("@nombre", SqlDbType.VarChar, 25) { Value = alumno.Nombre },
                    new SqlParameter("@apellido1", SqlDbType.VarChar, 50) { Value = alumno.Apellido1 },
                    new SqlParameter("@apellido2", SqlDbType.VarChar, 50) { Value = alumno.Apellido2 },
                    new SqlParameter("@ciudad", SqlDbType.VarChar, 25) { Value = alumno.Ciudad },
                    new SqlParameter("@direccion", SqlDbType.VarChar, 50) { Value = alumno.Direccion },
                    new SqlParameter("@telefono", SqlDbType.VarChar, 9) { Value = alumno.Telefono },
                    new SqlParameter("@fecha_nacimiento", SqlDbType.Date) { Value = alumno.FechaNacimiento },
                    new SqlParameter("@sexo", SqlDbType.Char, 1) { Value = alumno.Sexo }
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_Insertar_Alumno @nif, @nombre, @apellido1, @apellido2, @ciudad, @direccion, @telefono, @fecha_nacimiento, @sexo",
                    parametros);

                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            var alumno = await _context.Alumnos
                .FromSqlRaw("EXEC SP_Consultar_Alumno_ID @id", param)
                .FirstOrDefaultAsync();

            if (alumno == null) return NotFound();

            return View(alumno);
        }

        // POST: Alumno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Alumno alumno)
        {
            if (id != alumno.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var parametros = new[]
                {
                    new SqlParameter("@id", SqlDbType.Int) { Value = alumno.Id },
                    new SqlParameter("@nif", SqlDbType.VarChar, 9) { Value = alumno.Nif },
                    new SqlParameter("@nombre", SqlDbType.VarChar, 25) { Value = alumno.Nombre },
                    new SqlParameter("@apellido1", SqlDbType.VarChar, 50) { Value = alumno.Apellido1 },
                    new SqlParameter("@apellido2", SqlDbType.VarChar, 50) { Value = alumno.Apellido2 },
                    new SqlParameter("@ciudad", SqlDbType.VarChar, 25) { Value = alumno.Ciudad },
                    new SqlParameter("@direccion", SqlDbType.VarChar, 50) { Value = alumno.Direccion },
                    new SqlParameter("@telefono", SqlDbType.VarChar, 9) { Value = alumno.Telefono },
                    new SqlParameter("@fecha_nacimiento", SqlDbType.Date) { Value = alumno.FechaNacimiento },
                    new SqlParameter("@sexo", SqlDbType.Char, 1) { Value = alumno.Sexo }
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC SP_Actualizar_Alumno @id, @nif, @nombre, @apellido1, @apellido2, @ciudad, @direccion, @telefono, @fecha_nacimiento, @sexo",
                    parametros);

                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            var alumno = await _context.Alumnos
                .FromSqlRaw("EXEC SP_Consultar_Alumno_ID @id", param)
                .FirstOrDefaultAsync();

            if (alumno == null) return NotFound();
            return View(alumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var param = new SqlParameter("@id", SqlDbType.Int) { Value = id };
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_Eliminar_Alumno @id", param);
            return RedirectToAction(nameof(Index));
        }
    }
}
