using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            // builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
             //builder.Property(x => x.OriginalPrice).IsRequired().HasMaxLength(100);

            //builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);

            //builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            //builder.Property(p => p.OriginalPrice).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.ProductTypeId);
        }
    }
}