using Common.ServiceHelpers.Abstraction.DependencyInjection;
using System.Data;

namespace Common.Repositories
{
    public interface IUnitOfWork : IScopedLifeTime
    {
        Task Save();
        Task CommitTransaction();
        Task RollbackTransaction();
        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task DoTransaction(Func<Task> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
