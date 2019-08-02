using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using Contoso.API.Infrastructure;

namespace Contoso.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            AreaRegistration.RegisterAllAreas();
            AutoMapperConfig.RegisterMappings();
            // Web API configuration and services
            config.Filters.Add(new ContosoApiException());

            //The handler, like the logger, must be registered in the Web API configuration. 
            //Note that we can only have one Exception Handler per application.
            config.Services.Replace(typeof(IExceptionHandler), new ContosoApiExceptionHandler());

            // will return json format instead of XML (especially in chrome browser)
             config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            // attribute routing in web api
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}