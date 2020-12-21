using Microsoft.EntityFrameworkCore;
using PersonApi.Core.Domain;

namespace PersonApi.Persistence
{
    public class PersonDbContext :  DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options):base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
    }
}
