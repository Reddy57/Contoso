using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Contoso.API.DTO;
using Contoso.API.Utilities;
using Contoso.Model;
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
        public IHttpActionResult GetDepartments([FromUri] int pageSize = 8, [FromUri] int page = 1,
                                                [FromUri] string name = "")
        {
            var totalDepartmentsCount = _departmentService.GetTotalDepartments();
            var departments = _departmentService.GetDepartmentsPagination(pageSize, page, name);
            var pagedDepartments = new PagedResultSet<DepartmentDTO>(page, pageSize, totalDepartmentsCount,
                                                                     Mapper
                                                                         .Map<IList<Department>, IList<DepartmentDTO>
                                                                         >(departments.ToList()));
            var response = pagedDepartments.Data.Any()
                ? Request.CreateResponse(HttpStatusCode.OK, pagedDepartments)
                : Request.CreateResponse(HttpStatusCode.NotFound, "No Departments for your query");
            return ResponseMessage(response);
        }
    }
}