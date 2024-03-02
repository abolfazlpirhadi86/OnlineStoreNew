using Common.Entity;
using Common.Services.Abstraction.DependencyInjection;

namespace Common.Services.Base
{
    public interface IGenericService<TEntity, ListDTO, CreateDTO, DTO, TId> : IScopedLifeTime
       where TEntity : BaseEntity<TId>
       where ListDTO : class
       where CreateDTO : class
       where DTO : class
    {
        Task<TId> Create(CreateDTO model);
        Task Update(DTO model);
        Task Remove(TId id);
        Task<DTO> Find(TId id);
        //Task<PagingResult<ListDTO>> FindAll(PagingFilter model);
    }
}
