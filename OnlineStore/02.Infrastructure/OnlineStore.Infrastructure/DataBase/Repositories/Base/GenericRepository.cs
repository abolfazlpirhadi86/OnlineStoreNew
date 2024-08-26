using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OnlineStore.Application.IRepositories.Base;
using OnlineStore.Common.Entity;
using OnlineStore.Common.Exceptions;
using OnlineStore.Common.Messages;
using OnlineStore.Common.Services;
using System.Linq.Expressions;

namespace OnlineStore.Infrastructure.DataBase.Repositories.Base
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : BaseEntity<TId>, new()
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapperService _mapperService;
        public GenericRepository(ApplicationDbContext dbContext, IMapperService mapperService)
        {
            _dbContext = dbContext;
            _mapperService = mapperService;
        }

        #region Add
        public async Task Add(TEntity model)
        {
            await _dbContext.AddAsync(model);
        }
        public async Task Add<DTO>(DTO model)
        {
            await _dbContext.AddAsync(_mapperService.Map<TEntity>(model));
        }
        public async Task AddRange(List<TEntity> model)
        {
            await _dbContext.AddRangeAsync(model);
        }
        public async Task AddRange<DTO>(List<DTO> model)
        {
            await _dbContext.AddRangeAsync(_mapperService.Map<List<TEntity>>(model));
        }
        #endregion

        #region Update
        public async Task Update(TEntity model)
        {
            //if (EntityHasLogAttribute(model))
            //{
            //    var entity = await _dbContext.Set<TEntity>().AsTracking().FirstOrDefaultAsync(e => e.Id.Equals(model.Id));
            //    _dbContext.Update(_mapperService.Map(model, entity));
            //    UpdateChangeTrackerForRowVersion(model, model.Id.ToString());
            //}
            //else
            //{
                _dbContext.Update(model);
            //}
        }
        public async Task Update<DTO>(DTO model)
        {
            var id = model.GetType().GetProperties().Where(e => e.Name.ToLower() == "id").First().GetValue(model);
            var entity = await _dbContext.Set<TEntity>().AsTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (entity == null)
                throw new BusinessException(Messages.RecordNotFound);

            _dbContext.Update(_mapperService.Map(model, entity));
            UpdateChangeTrackerForRowVersion(model, id.ToString());
        }
        public async Task UpdateRange(List<TEntity> model)
        {
            if (model.Any())
            {
                if (EntityHasLogAttribute(model[0]))
                {
                    var entities = await _dbContext.Set<TEntity>().AsTracking().Where(e => model.Select(i => i.Id).ToList().Distinct().Contains(e.Id)).ToListAsync();
                    model.ForEach(e =>
                    {
                        var entity = entities.Find(i => i.Id.Equals(e.Id));
                        _dbContext.Update(_mapperService.Map(e, entity));
                        UpdateChangeTrackerForRowVersion(e, entity.Id.ToString());
                    });
                }
                else
                {
                    _dbContext.UpdateRange(model);
                }
            }
        }
        #endregion

        #region Remove
        public async Task Remove(TId id)
        {
            if (!await _dbContext.Set<TEntity>().AnyAsync(e => e.Id.Equals(id)))
                throw new BusinessException(Messages.RecordNotFound);
            _dbContext.Remove(new TEntity { Id = id });
        }
        public async Task SoftRemove(TId id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id) ?? throw new BusinessException(Messages.RecordNotFound);
            _dbContext.Entry(entity).Property("IsRemoved").CurrentValue = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task Remove(TEntity model)
        {
            _dbContext.Remove(model);
        }
        public async Task Remove<DTO>(DTO model)
        {
            _dbContext.Remove(_mapperService.Map<TEntity>(model));
        }
        public async Task SoftRemove(TEntity model)
        {
            _dbContext.Entry(model).Property("IsRemoved").CurrentValue = true;
            _dbContext.Entry(model).State = EntityState.Modified;
        }
        public async Task SoftRemove<DTO>(DTO model)
        {
            var id = model.GetType().GetProperties().Where(e => e.Name.ToLower() == "id").First().GetValue(model);
            var entity = await _dbContext.Set<TEntity>().FindAsync(id) ?? throw new BusinessException(Messages.RecordNotFound);
            _dbContext.Entry(entity).Property("IsRemoved").CurrentValue = true;
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public async Task RemoveRange(List<TEntity> model)
        {
            _dbContext.RemoveRange(model);
        }
        public async Task RemoveRange<DTO>(List<DTO> model)
        {
            var entityList = _mapperService.Map<List<TEntity>>(model);
            _dbContext.RemoveRange(entityList);
        }
        public async Task SoftRemoveRange(List<TEntity> model)
        {
            model.ForEach(e =>
            {
                _dbContext.Entry(e).Property("IsRemoved").CurrentValue = true;
                _dbContext.Entry(e).State = EntityState.Modified;
            });
        }
        #endregion

        #region Exists
        public async Task<bool> Exist(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbContext.Set<TEntity>().AnyAsync(expression);
        }
        #endregion

        #region AddOrUpdate
        public async Task AddOrUpdate<DTO>(DTO model)
        {
            var id = model.GetType().GetProperties().Where(e => e.Name.ToLower() == "id").First().GetValue(model);
            var entity = await _dbContext.Set<TEntity>().AsTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (entity == null)
            {
                await _dbContext.AddAsync(_mapperService.Map<TEntity>(model));
            }
            else
            {
                _dbContext.Update(_mapperService.Map(model, entity));
                UpdateChangeTrackerForRowVersion(model, id.ToString());
            }
        }
        #endregion

        #region Find
        public async Task<TEntity> Find(TId id)
        {
            var result = await _dbContext.Set<TEntity>().FindAsync(id);
            return result;
        }
        public async Task<DTO> Find<DTO>(TId id)
        {
            var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(e => e.Id.Equals(id))).FirstOrDefaultAsync();
            return result;
        }
        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> condition)
        {
            var result = await _dbContext.Set<TEntity>().Where(condition).FirstOrDefaultAsync();
            return result;
        }
        public async Task<DTO> Find<DTO>(Expression<Func<TEntity, bool>> condition)
        {
            var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(condition)).FirstOrDefaultAsync();
            return result;
        }
        public async Task<TEntity> Find(TId id, params string[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().Where(e => e.Id.Equals(id));
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.FirstOrDefaultAsync();
            return result;
        }
        public async Task<TEntity> Find(TId id, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().Where(e => e.Id.Equals(id));
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.FirstOrDefaultAsync();
            return result;
        }
        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().Where(condition);
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.FirstOrDefaultAsync();
            return result;
        }
        public async Task<TEntity> Find(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().Where(condition);
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.FirstOrDefaultAsync();
            return result;
        }
        #endregion

        #region FindAll
        public async Task<List<TEntity>> FindAll()
        {
            var result = await _dbContext.Set<TEntity>().ToListAsync();
            return result;
        }
        public async Task<List<DTO>> FindAll<DTO>()
        {
            var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>()).ToListAsync();
            return result;
        }
        public async Task<List<TEntity>> FindAll(params string[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.ToListAsync();
            return result;
        }
        public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition)
        {
            var result = await _dbContext.Set<TEntity>().Where(condition).ToListAsync();
            return result;
        }
        public async Task<List<DTO>> FindAll<DTO>(Expression<Func<TEntity, bool>> condition)
        {
            var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(condition)).ToListAsync();
            return result;
        }
        public async Task<List<TEntity>> FindAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().AsQueryable();
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.ToListAsync();
            return result;
        }
        public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params string[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().Where(condition).AsQueryable();
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.ToListAsync();
            return result;
        }
        public async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            var iqueryable = _dbContext.Set<TEntity>().Where(condition).AsQueryable();
            foreach (var item in navigationProperties)
            {
                iqueryable = iqueryable.Include(item);
            }
            var result = await iqueryable.ToListAsync();
            return result;
        }
        #endregion

        #region FindAllPaging
        //public async Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingFilter filter) where DTO : class
        //{
        //    var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>()).FilterPaging(filter);
        //    return result;
        //}
        //public async Task<PagingResult<DTO>> FindAllPaging<DTO>(PagingFilter filter, Expression<Func<TEntity, bool>> condition) where DTO : class
        //{
        //    var result = await _mapperService.ProjectTo<DTO>(_dbContext.Set<TEntity>().Where(condition)).FilterPaging(filter);
        //    return result;
        //}
        #endregion

        #region Save
        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion

        #region PrivateMethods
        private bool EntityHasLogAttribute(TEntity entity)
        {
            var result = true;// Constants.LoggedEntities.Where(e => e.Value == LogBehaviour.Update || e.Value == LogBehaviour.UpdateAndDelete).Select(e => e.Key).ToList().Contains(entity.GetType().FullName);
            return result;
        }

        private void UpdateChangeTrackerForRowVersion(object inputEntity, string Id)
        {
            var inputRowVersion = inputEntity.GetType().GetProperties().FirstOrDefault(e => e.Name == "RowVersion");
            if (inputRowVersion != null)
            {
                var entityChangeTracker = _dbContext.ChangeTracker.Entries<TEntity>().First(e => e.Entity.Id.ToString() == Id);
                //var originalValue = entityChangeTracker.Properties.First(e => e.Metadata.Name == "RowVersion").OriginalValue;
                //var inputRowVersionValue = inputRowVersion.GetValue(inputEntity);
                entityChangeTracker.Properties.First(e => e.Metadata.Name == "RowVersion").OriginalValue = inputRowVersion.GetValue(inputEntity);
            }
        }

        public Task RemoveRange(Expression<Func<TEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }

        


        #endregion
    }
}
