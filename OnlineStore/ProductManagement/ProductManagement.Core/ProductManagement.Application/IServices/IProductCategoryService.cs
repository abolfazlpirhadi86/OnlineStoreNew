using Common.ServiceHelpers.Abstraction.DependencyInjection;
using Common.Services;
using ProductManagement.Domain.DTOs.ProductCategory;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.IServices
{
    public interface IProductCategoryService : IGenericService<ProductCategory, ListProductCategoryDTO,
        CreateProductCategoryDTO, ProductCategoryDTO, long>, IScopedLifeTimeWithInterceptor
    {
        
    }
}
