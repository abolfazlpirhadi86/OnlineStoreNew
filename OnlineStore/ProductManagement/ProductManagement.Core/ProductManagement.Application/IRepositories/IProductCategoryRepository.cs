using Common.Repositories.Base;
using Common.Services.Abstraction.DependencyInjection;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.IRepositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory, long>, IScopedLifeTime
    {
    }
}
