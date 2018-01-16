using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Data.Repositories;
using Contoso.Model;

namespace Contoso.Service
{
   public class DepartmentService: IDepartmentService
   {
       private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
           return _departmentRepository.GetAll();
        }

       public IEnumerable<Department> GetAllDepartmentsIncludeCourses()
       {
           return _departmentRepository.GetAllDepartmentsIncludeCourses();
          // return _departmentRepository.GetAllDepartmentsLazyCourses();
       }
   }

   public interface IDepartmentService
   {
       IEnumerable<Department> GetAllDepartments();
       IEnumerable<Department> GetAllDepartmentsIncludeCourses();
    }
}
