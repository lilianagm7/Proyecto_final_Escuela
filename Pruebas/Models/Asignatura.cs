using System;
using System.Collections.Generic;

namespace Pruebas.Models
{
    public partial class Asignatura
    {
        public Asignatura()
        {
            AlumnoSeMatriculaAsignaturas = new HashSet<AlumnoSeMatriculaAsignatura>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public double? Creditos { get; set; }
        public string? Tipo { get; set; }
        public byte? Curso { get; set; }
        public byte? Cuatrimestre { get; set; }
        public int? IdProfesor { get; set; }
        public int? IdGrado { get; set; }

        public virtual Grado? IdGradoNavigation { get; set; }
        public virtual Profesor? IdProfesorNavigation { get; set; }
        public virtual ICollection<AlumnoSeMatriculaAsignatura> AlumnoSeMatriculaAsignaturas { get; set; }
    }
}
