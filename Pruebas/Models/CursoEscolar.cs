using System;
using System.Collections.Generic;

namespace Pruebas.Models
{
    public partial class CursoEscolar
    {
        public CursoEscolar()
        {
            AlumnoSeMatriculaAsignaturas = new HashSet<AlumnoSeMatriculaAsignatura>();
        }

        public int Id { get; set; }
        public int? AnyoInicio { get; set; }
        public int? AnyoFin { get; set; }

        public virtual ICollection<AlumnoSeMatriculaAsignatura> AlumnoSeMatriculaAsignaturas { get; set; }
    }
}
