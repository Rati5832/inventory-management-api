using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class ProductSupplierConfiguration : IEntityTypeConfiguration<ProductSupplier>
{
    public void Configure(EntityTypeBuilder<ProductSupplier> builder)
    {
        builder.ToTable("ProductSuppliers");

        builder.HasKey(productSupplier => new
        {
            productSupplier.ProductId,
            productSupplier.SupplierId
        });

        builder.HasOne(productSupplier => productSupplier.Product)
            .WithMany(product => product.ProductSuppliers)
            .HasForeignKey(productSupplier => productSupplier.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(productSupplier => productSupplier.Supplier)
            .WithMany(supplier => supplier.ProductSuppliers)
            .HasForeignKey(productSupplier => productSupplier.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
