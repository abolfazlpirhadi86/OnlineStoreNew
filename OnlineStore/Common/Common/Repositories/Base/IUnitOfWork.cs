using Common.Services.Abstraction.DependencyInjection;
using System.Data;

namespace Common.Repositories.Base
{
    public interface IUnitOfWork : IScopedLifeTime
    {
        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task CommitTransaction();
        Task RollbackTransaction();
        Task Save();
        Task DoTransaction(Func<Task> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
