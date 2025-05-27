using System;
using System.Collections.Generic;

namespace Pruebas.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Profesors = new HashSet<Profesor>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Profesor> Profesors { get; set; }
    }
}
