using Common.Messages;
using Common.Results;
using Common.ServiceHelpers;
using Common.Services;
using ProductManagement.Application.IRepositories;
using ProductManagement.Application.IServices;
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

        //public async Task<OperationResult<int>> Add(CreateProductCategoryDTO model)
        //{
        //    var operation = new OperationResult();
        //    if (await _productCategoryRepository.Exist(x => x.Title == model.Title))
        //        return operation.Fail(Message.DuplicateRecode);

        //    //Slug

        //    await _productCategoryRepository.Add(model);
        //    await _productCategoryRepository.Save();

        //    return operation.Success();
        //}
    }
}
