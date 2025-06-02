using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pruebas.Models;

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
        public async Task<IActionResult> Index(string buscar)
        {
            var param = new Microsoft.Data.SqlClient.SqlParameter("@buscar",
                string.IsNullOrEmpty(buscar) ? DBNull.Value : buscar);

            var alumnos = await _context.Alumnos
                .FromSqlRaw("EXEC SP_Consultar_Alumno @buscar", param)
                .ToListAsync();

            ViewData["Buscar"] = buscar;

            return View(alumnos);
        }



        // GET: Alumno/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumno/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nif,Nombre,Apellido1,Apellido2,Ciudad,Direccion,Telefono,FechaNacimiento,Sexo")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var parametros = new[]{
                    new SqlParameter("@Nif", alumno.Nif ?? (object)DBNull.Value),
                    new SqlParameter("@Nombre", alumno.Nombre ?? (object)DBNull.Value),
                    new SqlParameter("@Apellido1", alumno.Apellido1 ?? (object)DBNull.Value),
                    new SqlParameter("@Apellido2", alumno.Apellido2 ?? (object)DBNull.Value),
                    new SqlParameter("@Ciudad", alumno.Ciudad ?? (object)DBNull.Value),
                    new SqlParameter("@Direccion", alumno.Direccion ?? (object)DBNull.Value),
                    new SqlParameter("@Telefono", alumno.Telefono ?? (object)DBNull.Value),
                    new SqlParameter("@FechaNacimiento", alumno.FechaNacimiento),
                    new SqlParameter("@Sexo", alumno.Sexo ?? (object)DBNull.Value)
                };
                await _context.Database.ExecuteSqlRawAsync("EXEC sp_InsertarAlumno @Nif, @Nombre, @Apellido1, @Apellido2, @Ciudad, @Direccion, @Telefono, @FechaNacimiento, @Sexo", parametros);

                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumno/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Alumno/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nif,Nombre,Apellido1,Apellido2,Ciudad,Direccion,Telefono,FechaNacimiento,Sexo")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
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
            return View(alumno);
        }

        // GET: Alumno/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alumnos == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var param = new Microsoft.Data.SqlClient.SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_EliminarAlumno @Id", param);

            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
          return (_context.Alumnos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Alumno/ConsultaAvanzada
        public IActionResult ConsultaAvanzada()
        {
            return View();
        }

        // POST: Alumno/ConsultaAvanzada
        [HttpPost]
        public async Task<IActionResult> ConsultaAvanzada(string apellido1, DateTime fechaNacimiento)
        {
            var parametros = new[]
            {
                new SqlParameter("@Apellido1", apellido1 ?? (object)DBNull.Value),
                new SqlParameter("@FechaNacimiento", fechaNacimiento)
            };

            var alumnos = await _context.Alumnos
                .FromSqlRaw("EXEC sp_ConsultarAlumnosPorApellidoYFechaNacimiento @Apellido1, @FechaNacimiento", parametros)
                .ToListAsync();

            return View(alumnos);
        }

        // GET: Alumno/ConsultaPorFecha
        public IActionResult ConsultaPorFecha()
        {
            return View();
        }

        // POST: Alumno/ConsultaPorFecha
        [HttpPost]
        public async Task<IActionResult> ConsultaPorFecha(DateTime fechaNacimiento)
        {
            var parametro = new SqlParameter("@FechaNacimiento", fechaNacimiento);

            var alumnos = await _context.Alumnos
                .FromSqlRaw("EXEC sp_ConsultarAlumnosPorFechaNacimiento @FechaNacimiento", parametro)
                .ToListAsync();

            return View(alumnos);
        }

        // GET: Alumno/ConsultaPorNif
        public IActionResult ConsultaPorNif()
        {
            return View();
        }

        // POST: Alumno/ConsultaPorNif
        [HttpPost]
        public async Task<IActionResult> ConsultaPorNif(string nif)
        {
            var parametro = new SqlParameter("@Nif", nif ?? (object)DBNull.Value);

            var alumnos = await _context.Alumnos
                .FromSqlRaw("EXEC sp_ConsultarAlumnosPorNif @Nif", parametro)
                .ToListAsync();

            return View(alumnos);
        }


    }
}
