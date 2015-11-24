
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IaziServerWeb.Models;

namespace IaziServerWeb
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            try
            {

                var usuario = Simple.VerificarUsuario(context.Password, Convert.ToInt32(context.UserName));

                if (usuario.idUsuario > 0)
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    identity.AddClaim(new Claim("sub", context.UserName));
                    identity.AddClaim(new Claim("role", usuario.roleUsuario));

                    context.Validated(identity);
                }
            }
            catch (Exception)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        }
    }
}