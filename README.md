# lock stock and auth

This Nuget package is created to simplify the following authentication in Asp.Net Identity:

 - Token based authentication for  html5 web apps to Web Api
 - Token based authentication for html5 web apps to OData
 - Cookie authentication for web sites (still to be completed, help would be appreciated)
---

## installation
To get started you need to install the Nuget package:
>Install-Package Lock-Stock-and-Auth

Remember to delete your old authentication configuration files, controllers and views.  Common files that need to be deleted are:
- App_Start/IdentityConfig.cs
- App_Start/Startup.Auth.cs
- Controllers/AccountController.cs
- Models/AccountBindingModels.cs
- Models/AccountViewModels.cs
- Models/IdentityModels.cs
- Providers/ApplicationOAuthProvider.cs
- Views/Account (Note you can migrate these views if you've already customized them)

Next you need to edit your OWIN Startup.cs file in the root of your web application to include the following:
```javascript
    public void Configuration(IAppBuilder app)
    {
        var config = new HttpConfiguration();
        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        app.UseWebApi(config);

        var lockStockOwinStartup = new LockStockAuth.OwinStartup();
        lockStockOwinStartup.Configuration(app);
    }
```
This initializes the OWIN authentication and it also enables [CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing).

Next you need to setup the IOC Configuration for the controllers from the Global.asax Application_Start event.  I'd recommend creating a IocConfig.cs file in your App_Startup folder like this:
```javascript
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
```
And then calling the IocConfig from global.asax
```javascript
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    
    // Setup OData
    ODataConfig.Register(GlobalConfiguration.Configuration);
    RouteConfig.RegisterRoutes(RouteTable.Routes);

    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };
    // Setup Autofac IOC
    IocConfig.Register(GlobalConfiguration.Configuration);

    GlobalConfiguration.Configuration.EnsureInitialized();
}
```
## Javascript client library
The javascript library is kept in a [separate repository](https://github.com/Halceyon/aspnet-auth).
To install the javascript client using:
 #### npm
```
npm install aspnet-auth --save
```

## Compiling the Nuget package from source
The Nuget package content is built using grunt and running src\gruntfile.js
This populates the content part of Nuget from the Sample Web Application.

## Special thanks
This project wouldn't be possible if it wasn't for this awesome article: 
[AngularJS Token Authentication using ASP.NET Web API 2, Owin, and Identity](http://bitoftech.net/2014/06/09/angularjs-token-authentication-using-asp-net-web-api-2-owin-asp-net-identity/) by Taiseer Joudeh.  Much of the server side code is based on this blog post.

The technique used to create the Nuget package using a gruntfile I'm pretty sure comes from [Scott Hanselman](http://www.hanselman.com), although I honestly can't remember.  Maybe if he comes across the package one day he can comment :)