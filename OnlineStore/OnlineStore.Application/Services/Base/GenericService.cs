using OnlineStore.Application.IRepositories.Base;
using OnlineStore.Application.IServices.Base;
using OnlineStore.Common.Entity;
using OnlineStore.Common.Services;
using System.Linq.Expressions;

namespace OnlineStore.Application.Services.Base
{
    public class GenericService<TEntity, AddDTO, DTO, TId> : IGenericService<TEntity, AddDTO, DTO, TId>
      where TEntity : BaseEntity<TId>, new()
      where AddDTO : class
      where DTO : class
    {
        private readonly IMapperService _mapperService;
        private readonly IGenericRepository<TEntity, TId> _genericRepository;
        public GenericService(IMapperService mapperService,IGenericRepository<TEntity, TId> genericRepository)
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
        public async Task<bool> Exist(Expression<Func<TEntity, bool>> expression)
        {
            return await _genericRepository.Exist(expression);
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
