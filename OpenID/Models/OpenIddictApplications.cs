using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class OpenIddictApplications
    {
        public OpenIddictApplications()
        {
            OpenIddictAuthorizations = new HashSet<OpenIddictAuthorizations>();
            OpenIddictTokens = new HashSet<OpenIddictTokens>();
        }

        public string Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string DisplayName { get; set; }
        public string LogoutRedirectUri { get; set; }
        public string RedirectUri { get; set; }
        public string Type { get; set; }

        public virtual ICollection<OpenIddictAuthorizations> OpenIddictAuthorizations { get; set; }
        public virtual ICollection<OpenIddictTokens> OpenIddictTokens { get; set; }
    }
}
