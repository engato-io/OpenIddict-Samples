using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class OfertaEducativa
    {
        public OfertaEducativa()
        {
            AlumnoInscrito = new HashSet<AlumnoInscrito>();
        }

        public int OfertaEducativaId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<AlumnoInscrito> AlumnoInscrito { get; set; }
    }
}
