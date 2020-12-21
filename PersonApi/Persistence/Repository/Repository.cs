using Microsoft.EntityFrameworkCore;
using PersonApi.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PersonApi.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task<T> Get(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return dbSet.ToList();
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
           return dbSet.Where(predicate);
        }

        public async Task Add(T entity)
        {
           dbSet.Add(entity);
        }

        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
