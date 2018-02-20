using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Facebook;

namespace LockStockAuth.Auth.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public FacebookAuthProvider()
        {
            OnApplyRedirect = context =>
            {
                var newRedirectUri = context.RedirectUri;

                context.Response.Redirect(newRedirectUri);
            };
        }
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }
    }
}
