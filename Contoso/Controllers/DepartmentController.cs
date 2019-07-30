using System.Linq;
using System.Web.Mvc;
using Contoso.Service;
using Contoso.ViewModels;

namespace Contoso.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly IInstructorService _instructorService;

        public DepartmentController(IDepartmentService departmentService, IInstructorService instructorService)
        {
            //int x = 0;
            //int y = 1;
            //int z = y / x;
            _departmentService = departmentService;
            _instructorService = instructorService;
        }

        // GET: Department
        public ActionResult Index()
        {
            //int x = 0;
            //int y = 1;
            //int z = y / x;
            var departments = _departmentService.GetAllDepartmentsIncludeCourses();
            return View(departments);
        }

        // GET: Department/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            var instructors = _instructorService.GetAllInstructors()
                                                .Select(n => new SelectListItem
                                                             {
                                                                 Text = n.FullName, Value = n.Id.ToString()
                                                             }).ToList();
            instructors.Insert(0,
                               new SelectListItem {Value = null, Text = @"--- select Instructor ---"});

            var departmentCreate = new CreateDepartmentViewModel
                                   {
                                       Instructors =
                                           instructors
                                   };


            return View(departmentCreate);
        }

        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(CreateDepartmentViewModel department)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Department/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Department/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}