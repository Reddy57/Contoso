using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso.Model;

namespace Contoso.Data.Repositories
{
   public class PersonRepository:GenericRepository<Person>,IPersonRepository
    {
        public PersonRepository(ContosoDbContext context) : base(context)
        {
        }

        public IEnumerable<Person> GetAllPeople()
        {
            var people = _dbContext.Persons.ToList();
            return people;
        }
    }

   public interface IPersonRepository : IRepository<Person>
   {
       IEnumerable<Person> GetAllPeople();
   }
}
