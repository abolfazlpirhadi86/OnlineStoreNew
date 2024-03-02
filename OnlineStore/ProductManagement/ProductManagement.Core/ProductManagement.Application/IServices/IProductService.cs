using Common.ServiceHelpers.Abstraction.DependencyInjection;
using Common.Services.Base;
using ProductManagement.Domain.DTOs.ProductCategory;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.IServices
{
    public interface IProductService : IGenericService<ProductCategory, ListProductCategoryDTO, CreateProductCategoryDTO, ProductCategoryDTO, long>, IScopedLifeTimeWithInterceptor
    {
        
    }
}
