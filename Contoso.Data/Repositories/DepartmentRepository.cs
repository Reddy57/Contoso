using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Contoso.Model;

namespace Contoso.Data.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
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

        public IEnumerable<Department> GetDepartmentsPagination(int pageSize = 8, int page = 1, string name = "")
        {
            var query = _dbContext.Departments.AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(a => a.Name.Contains(name));
            var departments = query.OrderBy(d => d.Name).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return departments;
        }
    }

    public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<Department> GetAllDepartmentsIncludeCourses();
        IEnumerable<Department> GetAllDepartmentsLazyCourses();
        IEnumerable<Department> GetDepartmentsPagination(int pageSize = 8, int pageIndex = 0, string name = "");
    }
}