using System;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Contoso.Infrastructure;
using Contoso.ViewModels;
using Newtonsoft.Json;

namespace Contoso
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                if (authTicket != null)
                {
                    var serializeModel = JsonConvert.DeserializeObject<ContosoPrincipleModel>(authTicket.UserData);

                    var newUser = new ContosoPrincipal(authTicket.Name)
                    {
                        PersonId = serializeModel.PersonId,
                        FirstName = serializeModel.FirstName,
                        LastName = serializeModel.LastName,
                        Roles = serializeModel.Roles
                    };

                    HttpContext.Current.User = newUser;
                }
            }
        }

        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    /* Exception filters are not global error handlers and this is an important reason that forces us to still rely 
        //        on Application_Error event. Some programmers don't even use the HandleError filter in their application at all
        //        and use only the Application_Error event for doing all the error handling and logging work

        //        The important problem we face in the Application_Error event is, once the program execution reaches this point then
        //        we are out of MVC context and because of that we can miss some context information related to the exception

        //        When we need a controller or action level exception handling then we can use the HandleError filter along with
        //        the Application_Error event else we can simply ignore the HandleError filter
        //      */

        //    var exception = Server.GetLastError();
        //    if (exception == null)
        //        return;
        //    var mail = new MailMessage {From = new MailAddress("automated@contoso.com")};
        //    mail.To.Add(new MailAddress("administrator@contoso.com"));
        //    mail.Subject = "Site Error at " + DateTime.Now;
        //    mail.Body = "Error Description: " + exception.Message;
        //    var server = new SmtpClient {Host = "your.smtp.server"};
        //    //  server.Send(mail);

        //    // Clear the error
        //    Server.ClearError();

        //    // Redirect to a landing page
        //    //  Response.Redirect("/Error/ErrorPage");
        //  //  HttpContext.Current.Response.RedirectToRoute("RouteError", new { Id = 404 });

        //}
    }
}