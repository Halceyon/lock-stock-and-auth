using System.Data.Entity;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Sample_MVC_Site.Auth;

[assembly: OwinStartupAttribute(typeof(Sample_MVC_Site.Startup))]
namespace Sample_MVC_Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            AuthConfig.ConfigureOAuth(app);
            AuthConfig.ConfigureAuth(app);

            ODataConfig.Register(config);
            WebApiConfig.Register(config);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, AuthContextConfiguration>());
        }
    }
}
