using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);
             builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");
             builder.Property(x => x.Fee).HasColumnType("decimal(18,2)");

            // builder.Property(x => x.Id).UseIdentityColumn();
        }
    }
}