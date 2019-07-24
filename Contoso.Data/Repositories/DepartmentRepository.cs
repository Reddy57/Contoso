using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Model;

namespace Contoso.Data.Repositories
{
   public class DepartmentRepository: GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ContosoDbContext context) : base(context)
        {
        }

        public IEnumerable<Department> GetAllDepartmentsIncludeCourses()
        {
            var departments = _dbContext.Departments.Include(d => d.Courses).ToList();
            return departments;
        }

        public IEnumerable<Department> GetAllDepartmentsLazyCourses()
        {
            var departments = _dbContext.Departments.ToList();
            return departments;
        }
    }

    public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<Department> GetAllDepartmentsIncludeCourses();
        IEnumerable<Department> GetAllDepartmentsLazyCourses();

    }
}
