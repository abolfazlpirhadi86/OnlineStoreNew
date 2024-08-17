using OnlineStore.Application.IServices.Base;
using OnlineStores.Domain.DTOs.Products.Category;
using OnlineStores.Domain.Entities.Products;

namespace OnlineStore.Application.IServices.Products
{
    public interface ICategoryService : IGenericService<Category, AddCategoryDTO, CategoryDTO, int>
    {

    }
}
