using System;
using System.Collections.Generic;

namespace OpenID.Models
{
    public partial class OpenIddictTokens
    {
        public string Id { get; set; }
        public string ApplicationId { get; set; }
        public string AuthorizationId { get; set; }
        public string Subject { get; set; }
        public string Type { get; set; }

        public virtual OpenIddictApplications Application { get; set; }
        public virtual OpenIddictAuthorizations Authorization { get; set; }
    }
}
