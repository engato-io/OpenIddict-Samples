using OpenID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenID
{
    public class Data
    {
        readonly UniverContext _context;
        public Data(UniverContext context)
        {
            _context = context;
        }

        public Usuario GetUser(string user)
        {
            int usuarioId = int.Parse(user);
            return _context.Usuario.Where(x=> x.UsuarioId == usuarioId).FirstOrDefault();
        }
    }
}
