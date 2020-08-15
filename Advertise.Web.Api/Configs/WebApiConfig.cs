using System.Web.Http;
using System.Web.Http.Dispatcher;
using Advertise.Web.StructureMap;

namespace Advertise.Web.Api.Configs
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
       //  config.MapHttpAttributeRoutes();

       //  config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
           
            config.Services.Replace(typeof(IHttpControllerActivator), new StructureMapServiceActivator(config));
        }
    }
}
