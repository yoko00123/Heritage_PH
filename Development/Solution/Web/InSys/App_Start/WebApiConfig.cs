using InSys.Helpers;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace InSys
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register( )
        {
            HttpConfiguration config = new HttpConfiguration();

            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.MapHttpAttributeRoutes();
             
            config.Routes.MapHttpRoute(
                name: "InSys",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Insert(0, new EncryptJsonMediaTypeFormatter());

            return config;
        }
    }
}
