using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable("Promotions");

            builder.HasKey(x => x.Id);
            // builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.DiscountAmount).HasColumnType("decimal(18,2)");

            builder.Property(x => x.Name).IsRequired();
        }
    }
}