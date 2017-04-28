using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class Genero
    {
        public Genero()
        {
            Alumno = new HashSet<Alumno>();
        }

        public int GeneroId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Alumno> Alumno { get; set; }
    }
}
