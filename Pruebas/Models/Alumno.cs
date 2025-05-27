using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pruebas.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            AlumnoSeMatriculaAsignaturas = new HashSet<AlumnoSeMatriculaAsignatura>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "El campo NIF es obligatorio.")]
        public  string  Nif { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo Apellido paterno es obligatorio.")]
        public string Apellido1 { get; set; }

        [Required(ErrorMessage = "El campo Apellido materno es obligatorio.")]
        public string Apellido2 { get; set; }
        public string? Ciudad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }

        public virtual ICollection<AlumnoSeMatriculaAsignatura> AlumnoSeMatriculaAsignaturas { get; set; }
    }
}
