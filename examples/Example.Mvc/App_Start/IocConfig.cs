using System;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace Example.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public class IocConfig
    {
        /// <summary>
        /// Autofac IOC for LockStockAuth Controllers
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(typeof(LockStockAuth.OwinStartup).Assembly);
            
            // Register your Web controllers.
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(LockStockAuth.OwinStartup).Assembly);
            
            var container = builder.Build();
            
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); // Set the MVC DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container); //Set the WebApi DependencyResolver
        }
    }
}
