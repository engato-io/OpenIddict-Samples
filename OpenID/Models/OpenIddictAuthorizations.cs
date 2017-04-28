using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class OpenIddictAuthorizations
    {
        public OpenIddictAuthorizations()
        {
            OpenIddictTokens = new HashSet<OpenIddictTokens>();
        }

        public string Id { get; set; }
        public string ApplicationId { get; set; }
        public string Scope { get; set; }
        public string Subject { get; set; }

        public virtual ICollection<OpenIddictTokens> OpenIddictTokens { get; set; }
        public virtual OpenIddictApplications Application { get; set; }
    }
}
