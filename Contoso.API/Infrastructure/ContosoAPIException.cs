using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Contoso.API.Infrastructure
{
    /*
     * Exception filters in ASP.NET Web API are similar to those in ASP.NET MVC. However, they are declared in a separate namespace and 
     * function separately. In particular, the HandleErrorAttribute class used in MVC does not handle exceptions thrown by Web API controllers
     * 
    */

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ContosoApiException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            switch (actionExecutedContext.Exception)
            {
                case NotImplementedException _:
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                                                     {
                                                         Content =
                                                             new StringContent(actionExecutedContext.Exception.Message),
                                                         ReasonPhrase =
                                                             "Method is Not Implemented, Will implement in later version"
                                                     };
                    break;
                case ArgumentNullException _:
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                                                     {
                                                         Content =
                                                             new StringContent(actionExecutedContext.Exception.Message),
                                                         ReasonPhrase = "Please check your request "
                                                     };
                    break;
                default:
                    actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                                                     {
                                                         Content =
                                                             new StringContent(actionExecutedContext.Exception.Message),
                                                         ReasonPhrase = "Something went wrong, try again later "
                                                     };
                    break;
            }
        }
    }
}