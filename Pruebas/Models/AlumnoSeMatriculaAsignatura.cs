using System;
using System.Collections.Generic;

namespace Pruebas.Models
{
    public partial class AlumnoSeMatriculaAsignatura
    {
        public int IdAlumno { get; set; }
        public int IdAsignatura { get; set; }
        public int IdCursoEscolar { get; set; }

        public virtual Alumno IdAlumnoNavigation { get; set; } = null!;
        public virtual Asignatura IdAsignaturaNavigation { get; set; } = null!;
        public virtual CursoEscolar IdCursoEscolarNavigation { get; set; } = null!;
    }
}
