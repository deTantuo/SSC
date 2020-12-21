using PersonApi.Core;
using PersonApi.Core.IRepositories;
using PersonApi.Persistence.Repository;
using System.Threading.Tasks;

namespace PersonApi.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonDbContext _db;

        public UnitOfWork(PersonDbContext db)
        {
            _db = db;
            Person = new PersonRepository(_db);
        }

        public IPersonRepository Person { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
