using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class UsuarioDetalle
    {
        public int UsuarioId { get; set; }
        public string Password { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
