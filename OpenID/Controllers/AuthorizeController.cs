using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Authentication;
using System.Security.Claims;
using AspNet.Security.OpenIdConnect.Extensions;
using OpenIddict.Core;
using OpenID.Models;
using OpenID.Business;

namespace OpenID.Controllers
{
    //[Produces("application/json")]
    //[Route("api/Authorize")]
    public class AuthorizeController : Controller
    {
        UniverContext _context;

        public AuthorizeController(UniverContext context)
        {
            _context = context;
        }

        [HttpPost("~/connect/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                var user = _context.Usuario.SingleOrDefault(x => x.UsuarioId == int.Parse(request.Username));

                if (user == null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid"
                    });
                }

                if (!new Business.UserBusiness(_context).CheckPassword(user, request.Password))
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid"
                    });
                }

                var ticket = await CreateTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            if (request.IsRefreshTokenGrantType())
            {
                var info = await HttpContext.Authentication.GetAuthenticateInfoAsync(OpenIdConnectServerDefaults.AuthenticationScheme);

                //string user = info.Principal.Claims.ToList().First(x => x.Type == "sub").Value;

                var user = new Business.UserBusiness(_context).GetUser(info.Principal);

                if(user== null)
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The refresh token is no longer valid."
                    });
                }

                #region Comentado
                //var xd = info.Principal.Claims;


                //var id = (ClaimsIdentity)info.Principal.Identity;

                //string[] arreglo = new string[100];
                //int contador = 0;

                //foreach (Claim a in xd)
                //{
                //    Console.WriteLine(a.Value);
                //    arreglo[contador] = $" {a.Value} {a.Type} {a.Issuer} {a.ValueType}";
                //    a.Properties.ToList().ForEach(x =>
                //    {
                //        arreglo[contador] += $"  Key:{x.Key}, Value:{x.Value}"; 
                //    });
                //    contador++;
                //}

                //var idx = id.FindFirst(System.Security.Claims.ClaimTypes.Name);

                #endregion Comentado  
                #region Comentado
                //if (info.Principal.Claims.Username != "engato@outlook.com")
                //{
                //    return BadRequest(new OpenIdConnectResponse
                //    {
                //        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                //        ErrorDescription = "The username/password couple is invalid"
                //    });
                //}

                //if (request.Password != "Cayen001+")
                //{
                //    return BadRequest(new OpenIdConnectResponse
                //    {
                //        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                //        ErrorDescription = "The username/password couple is invalid"
                //    });
                //}
                #endregion Comentado

                var ticket = await CreateTicketAsync(request, user, info.Properties);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }


        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, Usuario user, AuthenticationProperties properties = null)
        {
            var identity =
                new ClaimsIdentity(
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role
                );

            identity.AddClaim(OpenIdConnectConstants.Claims.Subject, $"{user.UsuarioId}", OpenIdConnectConstants.Destinations.AccessToken);
            identity.AddClaim(OpenIdConnectConstants.Claims.Name, user.Nombre, OpenIdConnectConstants.Destinations.AccessToken);

            var principal = new ClaimsPrincipal(identity);

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal, properties, OpenIdConnectServerDefaults.AuthenticationScheme);


            if (!request.IsRefreshTokenGrantType())
            {
                // Set the list of scopes granted to the client application.
                ticket.SetScopes(new[]
                {
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Email,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess,
                    OpenIddictConstants.Scopes.Roles
                }.Intersect(request.GetScopes()));
            }
            ticket.SetResources("resource-server");

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in ticket.Principal.Claims)
            {
                //// Never include the security stamp in the access and identity tokens, as it's a secret value.
                //if (claim.Type == claims)
                //{
                //    continue;
                //}

                var destinations = new List<string>
                {
                    OpenIdConnectConstants.Destinations.AccessToken
                };

                // Only add the iterated claim to the id_token if the corresponding scope was granted to the client application.
                // The other claims will only be added to the access_token, which is encrypted when using the default format.
                if ((claim.Type == OpenIdConnectConstants.Claims.Name && ticket.HasScope(OpenIdConnectConstants.Scopes.Profile)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Email && ticket.HasScope(OpenIdConnectConstants.Scopes.Email)) ||
                    (claim.Type == OpenIdConnectConstants.Claims.Role && ticket.HasScope(OpenIddictConstants.Claims.Roles)))
                {
                    destinations.Add(OpenIdConnectConstants.Destinations.IdentityToken);
                }

                claim.SetDestinations(destinations);
                //claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken);
            }

            return ticket;
        }
    }
}