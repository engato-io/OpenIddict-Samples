using OpenID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenID.Business
{
    public class UserBusiness
    {
        private readonly UniverContext _context;

        public UserBusiness(UniverContext context)
        {
            _context = context;
        }

        public bool CheckPassword(Usuario User, string password)
        {
            return _context.UsuarioDetalle.Count(x => x.UsuarioId == User.UsuarioId && x.Password == password) > 0 ? true : false;
        }

        public Usuario GetUser(ClaimsPrincipal Claim)
        {
            return _context.Usuario.SingleOrDefault(x => x.UsuarioId == int.Parse(Claim.FindFirst(n => n.Type == "sub").Value));
        }
    }
}
