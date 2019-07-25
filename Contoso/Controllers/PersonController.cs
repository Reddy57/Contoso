using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Contoso.Service;
using Contoso.ViewModels;
using Newtonsoft.Json;
using Contoso.Utility;
namespace Contoso.Controllers
{
    
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        public ActionResult Index()
        {
            return View(_personService.GetAllPeople());
        }
       // GET: Login
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Person", null);
        }


        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Person/Create
        public ActionResult Register()
        {
            
            var model = new RegisterViewModel
            {
                States = Utility.Utility.GetAllStates()
            };
            return View(model);
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var person = _personService.GetValidPerson(loginViewModel.Username, loginViewModel.Password);

                    //person.Id = 6;
                    //person.FirstName = "Tony";
                    //person.LastName = "Stark";


                    if (person != null)
                    {
                        var personRoles = person.Roles.Select(r => r.RoleName).ToArray();

                        var serializeModel = new ContosoPrincipleModel
                        {
                            PersonId = person.Id,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            Roles = personRoles
                        };
                        
                        var userData = JsonConvert.SerializeObject(serializeModel);
                        var authTicket = new FormsAuthenticationTicket(1, person.Email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData);
                        var encTicket = FormsAuthentication.Encrypt(authTicket);
                        var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username and/or password");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}