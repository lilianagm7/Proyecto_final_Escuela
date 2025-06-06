﻿using System;
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
    public class ProfesorController : Controller
    {
        private readonly EscuelaBContext _context;

        public ProfesorController(EscuelaBContext context)
        {
            _context = context;
        }

        // GET: Profesor
        public async Task<IActionResult> Index()
        {
            var escuelaBContext = _context.Profesors.Include(p => p.IdDepartamentoNavigation);
            return View(await escuelaBContext.ToListAsync());
        }

        // GET: Profesor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Profesors == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesors
                .Include(p => p.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // GET: Profesor/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id");
            return View();
        }

        // POST: Profesor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nif,Nombre,Apellido1,Apellido2,Ciudad,Direccion,Telefono,FechaNacimiento,Sexo,IdDepartamento")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                var parameters = new[]{
                    new SqlParameter("@Nif", profesor.Nif ?? (object)DBNull.Value),
                    new SqlParameter("@Nombre", profesor.Nombre ?? (object)DBNull.Value),
                    new SqlParameter("@Apellido1", profesor.Apellido1 ?? (object)DBNull.Value),
                    new SqlParameter("@Apellido2", profesor.Apellido2 ?? (object)DBNull.Value),
                    new SqlParameter("@Ciudad", profesor.Ciudad ?? (object)DBNull.Value),
                    new SqlParameter("@Direccion", profesor.Direccion ?? (object)DBNull.Value),
                    new SqlParameter("@Telefono", profesor.Telefono ?? (object)DBNull.Value),
                    new SqlParameter("@FechaNacimiento", profesor.FechaNacimiento),
                    new SqlParameter("@Sexo", profesor.Sexo ?? (object)DBNull.Value),
                    new SqlParameter("@IdDepartamento", profesor.IdDepartamento)
                };
                await _context.Database.ExecuteSqlRawAsync("EXEC SP_Insertar_Profesor @Nif, @Nombre, @Apellido1, @Apellido2, @Ciudad, @Direccion, @Telefono, @FechaNacimiento, @Sexo, @IdDepartamento", parameters);
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", profesor.IdDepartamento);
            return View(profesor);
        }

        // GET: Profesor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profesors == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesors.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }

            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", profesor.IdDepartamento);
            return View(profesor);
        }

        // POST: Profesor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nif,Nombre,Apellido1,Apellido2,Ciudad,Direccion,Telefono,FechaNacimiento,Sexo,IdDepartamento")] Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var parameters = new[]{
                        new SqlParameter("@Id", profesor.Id),
                        new SqlParameter("@Nif", profesor.Nif ?? (object)DBNull.Value),
                        new SqlParameter("@Nombre", profesor.Nombre ?? (object)DBNull.Value),
                        new SqlParameter("@Apellido1", profesor.Apellido1 ?? (object)DBNull.Value),
                        new SqlParameter("@Apellido2", profesor.Apellido2 ?? (object)DBNull.Value),
                        new SqlParameter("@Ciudad", profesor.Ciudad ?? (object)DBNull.Value),
                        new SqlParameter("@Direccion", profesor.Direccion ?? (object)DBNull.Value),
                        new SqlParameter("@Telefono", profesor.Telefono ?? (object)DBNull.Value),
                        new SqlParameter("@FechaNacimiento", profesor.FechaNacimiento == default(DateTime) ? (object)DBNull.Value : profesor.FechaNacimiento),
                        new SqlParameter("@Sexo", profesor.Sexo ?? (object)DBNull.Value),
                        new SqlParameter("@IdDepartamento", profesor.IdDepartamento)
                    };
                    await _context.Database.ExecuteSqlRawAsync("EXEC SP_Actualizar_Profesor @Id, @Nif, @Nombre, @Apellido1, @Apellido2, @Ciudad, @Direccion, @Telefono, @FechaNacimiento, @Sexo, @IdDepartamento", parameters);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfesorExists(profesor.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "Id", profesor.IdDepartamento);
            return View(profesor);
        }

        // GET: Profesor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Profesors == null)
            {
                return NotFound();
            }

            var profesor = await _context.Profesors
                .Include(p => p.IdDepartamentoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profesor == null)
            {
                return NotFound();
            }

            return View(profesor);
        }

        // POST: Profesor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Profesors == null)
            {
                return Problem("Entity set 'EscuelaBContext.Profesors' is null.");
            }

            var parameter = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC SP_Eliminar_Profesor @Id", parameter);

            return RedirectToAction(nameof(Index));
        }

        private bool ProfesorExists(int id)
        {
            return (_context.Profesors?.Any(e => e.Id == id)).GetValueOrDefault();
        }

       
        public async Task<IActionResult> SearchByApellido1(string apellido1)
        {
            if (string.IsNullOrEmpty(apellido1))
            {
                return RedirectToAction(nameof(Index));
            }

            var param = new SqlParameter("@apellido1", apellido1);

            var profesores = await _context.Profesors
                .FromSqlRaw("EXEC SP_Consultar_Profesor_Apellido1 @apellido1", param)
                .Include(p => p.IdDepartamentoNavigation)
                .ToListAsync();

            return View("Index", profesores);
        }

        // Filtrar profesores por IdDepartamento
        public async Task<IActionResult> FilterByDepartamento(int? idDepartamento)
        {
            if (idDepartamento == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var param = new SqlParameter("@id_departamento", idDepartamento.Value);

            var profesores = await _context.Profesors
                .FromSqlRaw("EXEC SP_Consultar_Profesor_Por_Departamento @id_departamento", param)
                .Include(p => p.IdDepartamentoNavigation)
                .ToListAsync();

            return View("Index", profesores);
        }
    }
}
