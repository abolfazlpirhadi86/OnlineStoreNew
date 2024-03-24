using Common.Entity;
using Common.Services.Abstraction.DependencyInjection;

namespace Common.Services
{
    public interface IGenericService<TEntity, ListDTO, AddDTO, DTO, TId> : IScopedLifeTime
       where TEntity : BaseEntity<TId>
       where ListDTO : class
       where AddDTO : class
       where DTO : class
    {
        Task<TId> Add(AddDTO model);
        Task Update(DTO model);
        Task Remove(TId id);
        Task<DTO> Find(TId id);
        //Task<PagingResult<ListDTO>> FindAll(PagingFilter model);
    }
}
