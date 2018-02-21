using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Contoso
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {

            // The first parameter is the name for the route and it's completely optional
            // The second parameter is the interesting,  it's a pattern but not regular expression that
            // contains segments which will be filled by the parameters in the url. So the below pattern contains three segments:
            // { controller} and { action} and {id}. 
            // these segments are wrapped in curly braces and that means they can be matched by more than one values

            /*
              The RouteTable is the one that stores all the routes defined in the application. 
              The RouteTable contains property named Routes which is of type RouteCollection and it is where
              we add all our routes.
              From the Application_Start we pass this RouteCollection to the RegisterRoutes method.
              The MVC pipeline needs the controller and action route values
            */

            // If you want to use Attribute Routing, you have to enable it by calling MapMvcAttributeRoutes on
            //the RouteCollection for your app
            // (usually this is done in RouteConfig):
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
