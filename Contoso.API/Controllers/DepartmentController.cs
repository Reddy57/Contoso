using System.Net;
using System.Net.Http;
using System.Web.Http;
using Contoso.Service;

namespace Contoso.API.Controllers
{
    [RoutePrefix("api/departments")]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetDepartments([FromUri] int pageSize, [FromUri] int page = 1,
                                                [FromUri] string name = "")
        {
            var totalDepartmentsCount = _departmentService.GetTotalDepartments();
            var departments = _departmentService.GetDepartmentsPagination(pageSize, page, name);

            var response = totalDepartmentsCount > 0 && departments != null
                ? Request.CreateResponse(HttpStatusCode.OK, departments)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Departments");
            return ResponseMessage(response);
        }
    }
}