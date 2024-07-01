using Common.Entity;

namespace Common.Repositories
{
    public interface IDapperRepository : IDisposable
    {
        void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task<T> Find<T>(long id);
    }
}
