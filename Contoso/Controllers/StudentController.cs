using System;
using System.Web.Mvc;
using Contoso.Model;
using Contoso.Service;
using Contoso.ViewModels;
using PagedList;

namespace Contoso.Controllers
{
    // [ContosoAuthorize(Roles = "Student")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: Student
        public ActionResult Index(int? page)
        {
            var pageNumber = (page ?? 1) - 1;
            const int pageSize = 10;
            var newStds = _studentService.GetStudentsByPagination(page, pageSize);
            var students = _studentService.GetAllStudents(page, pageSize, out var totalCount);
            var studentsAsIPagedList = new StaticPagedList<Student>(students, pageNumber + 1, pageSize, totalCount);
            return View(studentsAsIPagedList);
        }

        // GET: Student/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    var student = new Student
                    {
                        Email = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Password = model.Password,
                        EnrollmentDate = DateTime.Now,
                        State =  model.State
                    };

                    _studentService.CreateStudent(student);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Student/Edit/5
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

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Student/Delete/5
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