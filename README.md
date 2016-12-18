# lock stock and auth

This Nuget package is created to simplify the following authentication in Asp.Net Identity:

 - Cookie authentication for web sites
 - Token based authentication for html applications to Web Api
 - Token based authentication for html applications to OData

---

## installation
To get started you need to install the Nuget package:
>Install-Package Lock-Stock-and-Auth

This will create a folder in the root of your web application "Auth".
Inside this folder is the data context, controllers, models and views for your web application authentication.

To configure your oAuth and Auth option you need to edit the App_Start/AuthConfig.cs file. 

Next you need to edit your Startup.cs file in the root of your web application to include the following:
```javascript
    var config = new HttpConfiguration();

    AuthConfig.ConfigureOAuth(app);
    AuthConfig.ConfigureAuth(app);

    WebApiConfig.Register(config);
    app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
    app.UseWebApi(config);

    Database.SetInitializer(new MigrateDatabaseToLatestVersion<AuthContext, AuthContextConfiguration>());
```
This tells your web application to use the authentication options defined in App_Start\AuthConfig.cs and it also enables [CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing).

If you want to enable [CORS](https://en.wikipedia.org/wiki/Cross-origin_resource_sharing) for your web api, you need to edit your App_Start\WebApiConfig.cs file to look like this:

```javascript
public static void Register(HttpConfiguration config)
{
    var cors = new EnableCorsAttribute("*", "*", "*");
    config.EnableCors(cors);
    
    // Web API routes
    config.MapHttpAttributeRoutes();
    
    config.Routes.MapHttpRoute(
        name: "DefaultApi",
        routeTemplate: "api/{controller}/{id}",
        defaults: new { id = RouteParameter.Optional }
    );
    
    var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
    jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    
}
```
I use the jsonFormatter as I like all my web api replies to use [camelCase](https://en.wikipedia.org/wiki/Camel_case).

## HTML client library
When you install the Nuget package, it installs a client library for HTML applications to use in the root of your web application in js/libs/aspnetAuth.js

There is a sample HTML application called "Sample HTML Client" as part of the source code that demonstrates how to use authentication in a plain HTML application.  This is handy for HTML5 hybrid mobile applications.

## Compiling the Nuget package from source
The Nuget package content is built using grunt and running src\gruntfile.js
This populates the content part of Nuget from the Sample Web Application.

## Special thanks
This project wouldn't be possible if it wasn't for this awesome article: 
[AngularJS Token Authentication using ASP.NET Web API 2, Owin, and Identity](http://bitoftech.net/2014/06/09/angularjs-token-authentication-using-asp-net-web-api-2-owin-asp-net-identity/) by Taiseer Joudeh.  Much of the server side code is based on this blog post.

The technique used to create the Nuget package using a gruntfile I'm pretty sure comes from [Scott Hanselman](http://www.hanselman.com), although I honestly can't remember.  Maybe if he comes across the package one day he can comment :)