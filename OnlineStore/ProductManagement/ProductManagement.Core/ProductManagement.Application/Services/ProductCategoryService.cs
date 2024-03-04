using Common.Results;
using Common.ServiceHelpers;
using ProductManagement.Application.IRepositories;
using ProductManagement.Application.IServices;
using ProductManagement.Application.Services.Base;
using ProductManagement.Domain.DTOs.ProductCategory;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Services
{
    public class ProductCategoryService : GenericService<ProductCategory, ListProductCategoryDTO,
        CreateProductCategoryDTO, ProductCategoryDTO, long>, IProductCategoryService
    {
        private readonly IMapperService _mapperService;
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryService(IMapperService mapperService,
            IProductCategoryRepository productCategoryRepository)
            : base(mapperService, productCategoryRepository)
        {
            _mapperService = mapperService;
            _productCategoryRepository = productCategoryRepository;
        }

        public override async Task Create(CreateProductCategoryDTO model) 
        {
            var operationResult = new OperationResult();
            if(_productCategoryRepository.Add)
        } 
    }
}
