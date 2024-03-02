using Common.ServiceHelpers;
using ProductManagement.Application.IRepositories;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.Repositories.Base;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory, long>, IProductCategoryRepository
    {
        private readonly ProductManagementContext _dbContext;
        private readonly IMapperService _mapperService;
        public ProductCategoryRepository(ProductManagementContext dbContext, IMapperService mapperService)
            : base(dbContext, mapperService)
        {
            _dbContext = dbContext;
            _mapperService = mapperService;
        }
    }
}
