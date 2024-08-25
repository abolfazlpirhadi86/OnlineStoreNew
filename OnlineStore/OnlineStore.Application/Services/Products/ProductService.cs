using OnlineStore.Application.IRepositories.Products;
using OnlineStore.Application.IServices.Products;
using OnlineStore.Application.Services.Base;
using OnlineStore.Common.Exceptions;
using OnlineStore.Common.Extensions;
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

        public override async Task<int> Add(AddCategoryDTO model)
        {
            if (await _categoryRepository.Exist(x => x.Name == model.Name))
                throw new BusinessException();

            model.Slug = model.Slug.ToSlug();
            var category = _mapperService.Map<Category>(model);

            await _categoryRepository.Add(category);
            await _categoryRepository.Save();

            return category.Id;
        }
        public override async Task Update(CategoryDTO model)
        {
            var category = await _categoryRepository.Find(x => x.Id == model.Id) 
                ?? throw new BusinessException();

            if (await _categoryRepository.Exist(x => x.Name == model.Name && x.Id != model.Id))
                throw new BusinessException();

            model.Slug = model.Slug.ToSlug();
            var entity = _mapperService.Map<Category>(model);

            await _categoryRepository.Update(entity);
            await _categoryRepository.Save();
        }
    }
}
