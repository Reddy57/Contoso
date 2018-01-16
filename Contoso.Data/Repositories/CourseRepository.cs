using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Model;

namespace Contoso.Data.Repositories
{
   public class CourseRepository: GenericRepository<Course>, ICoursepository
    {
        public CourseRepository(ContosoDbContext context) : base(context)
        {
        }
    }

    public interface ICoursepository : IRepository<Course>
    {
    }
}
