using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Model;

namespace Contoso.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ContosoDbContext context) : base(context)
        {
            
        }

        public Student GetStudentByLastName(string lastName)
        {
            var student = _context.Persons.OfType<Student>().FirstOrDefault(s => s.LastName == lastName);
            return student;
        }
    }

    public interface IStudentRepository : IRepository<Student>
    {
        Student GetStudentByLastName(string lastName);
    }
}