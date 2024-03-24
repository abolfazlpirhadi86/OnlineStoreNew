using Common.Entity;
using Common.Repositories;
using Common.ServiceHelpers;

namespace Common.Services
{
    public class GenericService<TEntity, ListDTO, AddDTO, DTO, TId>
        : IGenericService<TEntity, ListDTO, AddDTO, DTO, TId>
      where TEntity : BaseEntity<TId>, new()
      where ListDTO : class
      where AddDTO : class
      where DTO : class
    {
        private readonly IMapperService _mapperService;
        private readonly IGenericRepository<TEntity, TId> _genericRepository;
        public GenericService(IMapperService mapperService,
            IGenericRepository<TEntity, TId> genericRepository)
        {
            _mapperService = mapperService;
            _genericRepository = genericRepository;
        }

        public virtual async Task<TId> Add(AddDTO model)
        {
            var entity = _mapperService.Map<TEntity>(model);
            await _genericRepository.Add(entity);
            await _genericRepository.Save();
            return entity.Id;
        }
        public virtual async Task Update(DTO model)
        {
            await _genericRepository.Update(model);
            await _genericRepository.Save();
        }
        public virtual async Task Remove(TId id)
        {
            await _genericRepository.Remove(id);
            await _genericRepository.Save();
        }
        public virtual async Task<DTO> Find(TId id)
        {
            var result = await _genericRepository.Find<DTO>(id);
            return result;
        }
        //public virtual async Task<PagingResult<DTO>> FindAll(PagingFilter model)
        //{
        //    var result = await _genericRepository.FindAllPaging<DTO>(model);
        //    return result;
        //}
    }
}
