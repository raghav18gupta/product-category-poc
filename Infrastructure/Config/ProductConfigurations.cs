using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(prop => prop.Name).IsRequired().HasMaxLength(500);
            builder.Property(prop => prop.Description).IsRequired();
            builder.Property(prop => prop.Price).HasColumnType("DECIMAL(18, 2)");
            builder.Property(prop => prop.PictureUrl).IsRequired();
            builder.HasOne(prop => prop.Category).WithMany()
                   .HasForeignKey(prop => prop.CategoryId);
        }
    }
}
