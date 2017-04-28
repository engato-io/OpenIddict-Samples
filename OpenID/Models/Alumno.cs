using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class Alumno
    {
        public Alumno()
        {
            AlumnoInscrito = new HashSet<AlumnoInscrito>();
        }

        public int AlumnoId { get; set; }
        public string Nombre { get; set; }
        public int GeneroId { get; set; }

        public virtual ICollection<AlumnoInscrito> AlumnoInscrito { get; set; }
        public virtual Genero Genero { get; set; }
    }
}
