using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStores.Domain.Entities.Products;

namespace OnlineStore.Infrastructure.Configurations.Products
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x=>x.Id);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(255).HasComment("نام");
            builder.Property(x => x.Picture).HasColumnType("varchar(11)").HasComment("تصویر");
            builder.Property(x => x.PictureTitle).HasColumnType("varchar(11)").HasComment("عنوان تصویر");
            builder.Property(x => x.PictureAlt).HasColumnType("varchar(11)").HasComment("Alt تصویر");
            builder.Property(x => x.Description).HasColumnType("varchar(11)").HasComment("توضیحات");
            builder.Property(x => x.Keywords).HasColumnType("varchar(11)").HasComment("کلمات کلیدی");
            builder.Property(x => x.MetaDescription).HasColumnType("varchar(11)").HasComment("توضیحات Meta");
            builder.Property(x => x.Slug).HasColumnType("nvarchar(300)").HasComment("اسلاگ");
        }
    }
}
