using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class AlumnoInscrito
    {
        public int AlumnoId { get; set; }
        public int OfertaEducativaId { get; set; }
        public int UsuarioId { get; set; }

        public virtual Alumno Alumno { get; set; }
        public virtual OfertaEducativa OfertaEducativa { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
