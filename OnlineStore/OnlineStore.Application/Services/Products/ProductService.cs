using OnlineStore.Application.IRepositories.Products;
using OnlineStore.Application.IServices.Products;
using OnlineStore.Application.Services.Base;
using OnlineStore.Common.Services;
using OnlineStores.Domain.DTOs.Products.Category;
using OnlineStores.Domain.Entities.Products;

namespace OnlineStore.Application.Services.Products
{
    public class ProductService : GenericService<Category, AddCategoryDTO, CategoryDTO, int>, ICategoryService
    {
        private readonly IMapperService _mapperService;
        private readonly ICategoryRepository _categoryRepository;
        public ProductService(IMapperService mapperService, ICategoryRepository categoryRepository)
            : base(mapperService, categoryRepository)
        {
            _mapperService = mapperService;
            _categoryRepository = categoryRepository;
        }
    }
}
