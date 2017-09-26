using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Contoso.API.Infrastructure;

namespace Contoso.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Filters.Add(new ContosoApiException());

            //The handler, like the logger, must be registered in the Web API configuration. 
            //Note that we can only have one Exception Handler per application.
            config.Services.Replace(typeof(IExceptionHandler), new ContosoApiExceptionHandler());


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional}
            );
        }
    }
}