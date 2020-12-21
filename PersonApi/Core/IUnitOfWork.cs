using PersonApi.Core.IRepositories;
using System;
using System.Threading.Tasks;

namespace PersonApi.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository Person { get; }

        Task SaveAsync();
    }
}
