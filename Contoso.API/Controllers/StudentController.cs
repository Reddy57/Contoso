using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contoso.API.Infrastructure;
using Contoso.Model;
using Contoso.Service;

namespace Contoso.API.Controllers
{
    [RoutePrefix("api/Students")]
   // [BasicAuthenticationFilter]
    public class StudentController : ApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/Student
        [Route("{page:int?}")]
        public HttpResponseMessage GetAllStudents(int? page)
        {
            var pageNumber = (page ?? 1) - 1;
            var totalCount = 0;
            var PageSize = 10;
            var students = _studentService.GetAllStudents(page, PageSize, out totalCount);

            var enumerable = students as IList<Student> ?? students.ToList();
            var response = enumerable.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, enumerable)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Students Found");

            return response;
        }

        [Route("{name}")]
        public IEnumerable<Student> GetStudentsByNameSearch(string name)
        {
            var students = _studentService.GetStudentByName(name);
            return students;
        }

        // GET: api/Student/5
        [ContosoApiException]
        [Route("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Student
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Student/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {
        }
    }
}