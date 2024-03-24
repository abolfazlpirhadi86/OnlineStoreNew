using Common.ServiceHelpers;
using Common.ServiceHelpers.Implementation.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.IRepositories;
using ProductManagement.Application.IServices;
using ProductManagement.Application.Services;
using ProductManagement.Infrastructure.Persistence.Repositories;

namespace ProductManagement.Infrastructure.Persistence
{
    public class ProductManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IMapperService,MapperService>();

            services.AddDbContext<ProductManagementContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
