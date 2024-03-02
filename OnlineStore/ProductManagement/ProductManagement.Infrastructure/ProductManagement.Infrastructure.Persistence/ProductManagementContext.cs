using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure.Persistence.DataBase.Configuration;

namespace ProductManagement.Infrastructure.Persistence
{
    public class ProductManagementContext : DbContext
    {
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public ProductManagementContext(DbContextOptions<ProductManagementContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(ProductCategoryConfiguration).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
