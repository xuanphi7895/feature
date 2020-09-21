using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config {
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory> {
        public void Configure (EntityTypeBuilder<ProductInCategory> builder) {
            builder.HasKey (t => new { t.CategoryId, t.ProductId });

            builder.ToTable ("ProductInCategories");

            // builder.HasOne (t => t.Product).WithMany (pc => pc.ProductInCategories)
            //     .HasForeignKey (pc => pc.Id);

            // builder.HasOne (t => t.Category).WithMany (pc => pc.ProductInCategories)
            //     .HasForeignKey (pc => pc.CategoryId);
        }
    }
}