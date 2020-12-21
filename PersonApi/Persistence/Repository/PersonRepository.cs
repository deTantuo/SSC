using PersonApi.Core.Domain;
using PersonApi.Core.IRepositories;
using PersonApi.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonApi.Persistence.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly PersonDbContext _db;

        public PersonRepository(PersonDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<IEnumerable<PersonList>> GetPersonList()
        {
            return _db.Person.Select(p => new PersonList()
            {
               name = p.name,
               state = p.state,
               birthDate = p.birthDate
            });
        }


        public async Task Update(PersonUpdateDTO updateDTO)
        {
            var personFromDb = _db.Person.FirstOrDefault(p => p.id == updateDTO.id);

            personFromDb.name = updateDTO.name;
            personFromDb.state = updateDTO.state;
            personFromDb.streetAddress = updateDTO.streetAddress;
            personFromDb.city = updateDTO.city;
            personFromDb.postalCode = updateDTO.postalCode;

            await _db.SaveChangesAsync();
        }
    }
}
