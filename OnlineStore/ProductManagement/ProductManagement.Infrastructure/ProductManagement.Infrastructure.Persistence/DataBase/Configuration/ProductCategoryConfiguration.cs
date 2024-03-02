using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using Common.Constants;

namespace ProductManagement.Infrastructure.Persistence.DataBase.Configuration
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasQueryFilter(x => x.IsRemoved == false);
            builder.HasComment("گروه محصولات");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).HasMaxLength(500).HasComment("تصویر");
            builder.Property(x => x.Title).IsRequired().HasColumnType(DbDataTypeConstants.TitleFa).HasComment("عنوان گروه");
            builder.Property(x => x.Description).HasColumnType(DbDataTypeConstants.Description).HasComment("توضیحات");
        }
    }
}
