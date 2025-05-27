using System;
using System.Collections.Generic;

namespace Pruebas.Models
{
    public partial class Grado
    {
        public Grado()
        {
            Asignaturas = new HashSet<Asignatura>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Asignatura> Asignaturas { get; set; }
    }
}
