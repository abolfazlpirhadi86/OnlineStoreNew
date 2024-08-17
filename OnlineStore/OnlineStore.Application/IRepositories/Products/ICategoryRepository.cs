using OnlineStore.Application.IRepositories.Base;
using OnlineStores.Domain.Entities.Products;

namespace OnlineStore.Application.IRepositories.Products
{
    public interface ICategoryRepository : IGenericRepository<Category, int>
    {

    }
}
