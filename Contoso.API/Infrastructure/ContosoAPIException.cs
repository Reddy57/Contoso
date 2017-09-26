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
            if (actionExecutedContext.Exception is NotImplementedException)
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "Method is Not Implemented, Will implement in later version"
                };
            else if (actionExecutedContext.Exception is ArgumentNullException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(actionExecutedContext.Exception.Message),
                    ReasonPhrase = "Please check your request "
                };
            }
          

        }
       
    }
}