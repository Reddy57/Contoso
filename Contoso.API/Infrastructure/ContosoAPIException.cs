using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Contoso.API.Infrastructure
{
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