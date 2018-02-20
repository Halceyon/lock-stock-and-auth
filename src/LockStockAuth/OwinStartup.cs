using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace LockStockAuth
{
    /// <summary>
    /// 
    /// </summary>
    public class OwinStartup
    {
        /// <summary>
        /// 
        /// </summary>
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            AuthConfig.ConfigureOAuth(app);
            AuthConfig.ConfigureAuth(app);

            DataProtectionProvider = app.GetDataProtectionProvider();
        }
    }
}
