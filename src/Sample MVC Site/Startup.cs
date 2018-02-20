using System.Data.Entity;
using System.Web.Http;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sample_MVC_Site.Startup))]
namespace Sample_MVC_Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            var lockStockOwinStartup = new LockStockAuth.OwinStartup();
            lockStockOwinStartup.Configuration(app);
        }
    }
}
