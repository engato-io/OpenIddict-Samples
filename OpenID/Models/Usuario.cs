using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AlumnoInscrito = new HashSet<AlumnoInscrito>();
        }

        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }

        public virtual ICollection<AlumnoInscrito> AlumnoInscrito { get; set; }
        public virtual UsuarioDetalle UsuarioDetalle { get; set; }
    }
}
