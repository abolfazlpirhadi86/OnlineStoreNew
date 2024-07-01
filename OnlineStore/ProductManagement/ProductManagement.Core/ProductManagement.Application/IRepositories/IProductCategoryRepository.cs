using Common.Repositories;
using Common.ServiceHelpers.Abstraction.DependencyInjection;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.IRepositories
{
    public interface IProductCategoryRepository : IGenericRepository<ProductCategory, long>, IScopedLifeTime
    {
    }
}
