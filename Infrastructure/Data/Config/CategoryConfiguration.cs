using Core.Entities;
using Core.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config {
    public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure (EntityTypeBuilder<Category> builder) {
                    
                    builder.ToTable ("Categories");
                    builder.HasKey (x => x.Id);
                    builder.Property(x => x.Status).HasDefaultValue(Status.Active);
                }
    }
}