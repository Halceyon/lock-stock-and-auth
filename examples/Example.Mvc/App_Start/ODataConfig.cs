using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Extensions;
using System.Web.OData.Builder;
using Example.Mvc.Data.Entities;

namespace Example.Mvc
{
    public class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Book>("books");
            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null); //new line
            builder.EnableLowerCamelCase();

            //// CORS
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
        }
    }
}