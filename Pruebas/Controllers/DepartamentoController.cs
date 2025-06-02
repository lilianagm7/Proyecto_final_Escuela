using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pruebas.Models;

namespace Pruebas.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly string _connectionString;

        public DepartamentoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Conexion");
        }

        // GET: Departamento
        public async Task<IActionResult> Index()
        {
            List<Departamento> lista = new();
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand cmd = new("SP_Consultar_Departamento", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    lista.Add(new Departamento
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    });
                }
            }
            return View(lista);
        }

        // GET: Departamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departamento departamento = null;
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand cmd = new("SP_Consultar_Departamento_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    departamento = new Departamento
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    };
                }
            }

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: Departamento/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new(_connectionString))
                {
                    SqlCommand cmd = new("sp_Insertar_Departamento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", departamento.Nombre);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departamento departamento = null;
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand cmd = new("SP_Consultar_Departamento_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    departamento = new Departamento
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    };
                }
            }

            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new(_connectionString))
                {
                    SqlCommand cmd = new("sp_Actualizar_Departamento", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", departamento.Id);
                    cmd.Parameters.AddWithValue("@nombre", departamento.Nombre);
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: Departamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departamento departamento = null;
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand cmd = new("SP_Consultar_Departamento_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                await conn.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    departamento = new Departamento
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Nombre = reader["nombre"].ToString()
                    };
                }
            }

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand cmd = new("SP_Eliminar_Departamento", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            bool existe = false;
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand cmd = new("SP_Consultar_Departamento_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    existe = reader.HasRows;
                }
            }
            return existe;
        }
    }
}
