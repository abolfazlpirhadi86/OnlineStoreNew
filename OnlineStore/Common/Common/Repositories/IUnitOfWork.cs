using Common.Services.Abstraction.DependencyInjection;
using System.Data;

namespace Common.Repositories
{
    public interface IUnitOfWork : IScopedLifeTime
    {
        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task DoTransaction(Func<Task> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransaction();
        Task RollbackTransaction();
        Task Save();
    }
}
