using OnlineStore.Common.Entity;
using System.Linq.Expressions;

namespace OnlineStore.Application.IRepositories.Base
{
    public interface IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>, new()
    {
        #region Add
        Task Add(TEntity model);
        Task Add<DTO>(DTO model);
        Task AddRange(List<TEntity> model);
        Task AddRange<DTO>(List<DTO> model);
        #endregion

        #region Update
        Task Update(TEntity model);
        Task Update<DTO>(DTO model);
        Task UpdateRange(List<TEntity> model);
        #endregion

        #region Remove
        Task Remove(TId id);
        Task SoftRemove(TId id);
        Task Remove(TEntity model);
        Task Remove<DTO>(DTO model);
        Task SoftRemove(TEntity model);
        Task SoftRemove<DTO>(DTO model);
        Task RemoveRange(List<TEntity> model);
        Task RemoveRange<DTO>(List<DTO> model);
        Task SoftRemoveRange(List<TEntity> model);
        //Task RemoveRange(Expression<Func<TEntity, bool>> condition);
        //Task SoftRemoveRange(Expression<Func<TEntity, bool>> condition);
        #endregion

        #region Find
        Task<TEntity> Find(TId id);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> condition);
        Task<TEntity> Find(TId id, params string[] navigationProperties);
        Task<TEntity> Find(TId id, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<DTO> Find<DTO>(TId id);
        Task<DTO> Find<DTO>(Expression<Func<TEntity, bool>> condition);
        #endregion

        #region Exists
        Task<bool> Exist(Expression<Func<TEntity, bool>> expression);
        #endregion

        #region AddOrUpdate
        Task AddOrUpdate<DTO>(DTO model);
        #endregion

        #region FindAll
        Task<List<TEntity>> FindAll();
        Task<List<DTO>> FindAll<DTO>();
        Task<List<TEntity>> FindAll(params string[] navigationProperties);
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition);
        Task<List<DTO>> FindAll<DTO>(Expression<Func<TEntity, bool>> condition);
        Task<List<TEntity>> FindAll(params Expression<Func<TEntity, object>>[] navigationProperties);
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties);
        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties);
        #endregion

        #region FindAllPaging
        //Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingFilter filter) where DTO : class;
        //Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingFilter filter, Expression<Func<TEntity, bool>> condition) where DTO : class;
        #endregion

        #region Save
        Task Save();
        #endregion
    }
}
