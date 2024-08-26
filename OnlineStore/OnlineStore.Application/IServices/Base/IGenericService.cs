using OnlineStore.Common.Entity;
using System.Linq.Expressions;

namespace OnlineStore.Application.IServices.Base
{
    public interface IGenericService<TEntity, AddDTO, DTO, TId>
       where TEntity : BaseEntity<TId>
       where AddDTO : class
       where DTO : class
    {
        Task<TId> Add(AddDTO model);
        Task Update(DTO model);
        Task Remove(TId id);
        Task<DTO> Find(TId id);
        Task<bool> Exist(Expression<Func<TEntity, bool>> expression); 
        //Task<PagingResult<DTO>> FindAll(PagingFilter model);
    }
}
