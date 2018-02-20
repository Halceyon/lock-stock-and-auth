using System.Web;
using LockStockAuth.Auth.Managers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace LockStockAuth
{
    public abstract class OwinContextBase
    {
        public IAuthenticationManager Authentication => HttpContext.Current.Request.GetOwinContext().Authentication;

        public string Id => Authentication.User.Identity.Name;

        public string Username => Authentication.User.Identity.Name;

        public ApplicationUserManager UserManager => HttpContext.Current.Request.GetOwinContext()
            .GetUserManager<ApplicationUserManager>();

        public ApplicationRoleManager RoleManager => HttpContext.Current.Request.GetOwinContext()
            .Get<ApplicationRoleManager>();
    }
}
