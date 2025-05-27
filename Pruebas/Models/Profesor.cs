using System;
using System.Collections.Generic;

namespace Pruebas.Models
{
    public partial class Profesor
    {
        public Profesor()
        {
            Asignaturas = new HashSet<Asignatura>();
        }

        public int Id { get; set; }
        public string? Nif { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
        public int? IdDepartamento { get; set; }

        public virtual Departamento? IdDepartamentoNavigation { get; set; }
        public virtual ICollection<Asignatura> Asignaturas { get; set; }
    }
}
