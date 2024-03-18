using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestaoProdutos.Domain.Entities;

namespace GestaoProdutos.Domain.Infra.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();

            builder.Property(u => u.Description)
                  .HasMaxLength(255)
                  .IsRequired();

            builder.Property(u => u.SupplierCode)
                 .HasMaxLength(255);

            builder
                .Property(x => x.SupplierDocument)
                .HasMaxLength(14);

            builder.Property(u => u.SupplierDescription)
                  .HasMaxLength(255);
        }
    }
}
