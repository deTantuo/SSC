using PersonApi.Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonApi.Core.IRepositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<IEnumerable<PersonList>> GetPersonList();
        Task Update(PersonUpdateDTO personDTO);
    }
}
