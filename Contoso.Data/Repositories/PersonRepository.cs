using System.Collections.Generic;
using System.Linq;
using Contoso.Model;

namespace Contoso.Data.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ContosoDbContext context) : base(context)
        {
        }

        public IEnumerable<Person> GetAllPeople()
        {
            var people = _dbContext.Persons.ToList();
            return people;

            // https://docs.microsoft.com/en-us/ef/ef6/querying/raw-sql
            //var people2 = _dbContext.Database.SqlQuery<Person>("Select * from People");
            //return people2;
        }
    }

    public interface IPersonRepository : IRepository<Person>
    {
        IEnumerable<Person> GetAllPeople();
    }
}