using Common.Entity;
using System.Linq.Expressions;

namespace Common.Repositories.Base
{
    public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>, new()
    {
        Task Add(TEntity model);
        Task Add<DTO>(DTO model);
        Task AddRange(List<TEntity> model);
        Task AddRange<DTO>(List<DTO> model);
        Task Update(TEntity model);
        Task Update<DTO>(DTO model);
        Task UpdateRange(List<TEntity> model);
        Task<bool> Exist(Expression<Func<TEntity, bool>> condition);
        Task AddOrUpdate<DTO>(DTO model);
        Task Remove(TId id);
        Task Remove(TEntity model);
        Task Remove<DTO>(DTO model);
        Task RemoveRange(List<TEntity> model);
        Task RemoveRange<DTO>(List<DTO> model);
        Task RemoveRange(Expression<Func<TEntity, bool>> condition);
        Task SoftRemove(TId id);
        Task SoftRemove(TEntity model);
        Task SoftRemove<DTO>(DTO model);
        Task SoftRemoveRange(List<TEntity> model);
        Task SoftRemoveRange(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> Find(TId id);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> Find(TId id, params string[] navigationProperties);
        Task<TEntity> Find(TId id, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<DTO> Find<DTO>(TId id);
        Task<DTO> Find<DTO>(Expression<Func<TEntity, bool>> condition);
        Task<List<TEntity>> FindAll();
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition);
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties);
        Task<List<TEntity>> FindAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<List<TEntity>> FindAll(params string[] navigationProperties);
        Task<List<DTO>> FindAll<DTO>();
        Task<List<DTO>> FindAll<DTO>(Expression<Func<TEntity, bool>> condition);
        //Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingFilter filter) where DTO : class;
        //Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingFilter filter, Expression<Func<TEntity, bool>> condition) where DTO : class;
        Task Save();
    }
}
