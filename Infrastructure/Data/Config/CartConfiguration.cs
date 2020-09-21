using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class CartConfiguration
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");
            builder.HasKey(x => x.Id);

            // builder.Property(x => x.Id).UseIdentityColumn();


            // builder.HasOne(x => x.Product).WithMany(x => x.Carts).HasForeignKey(x => x.Id);

        }
    }
}