using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class ProductFileConfiguration : IEntityTypeConfiguration<ProductFile>
{
    public void Configure(EntityTypeBuilder<ProductFile> builder)
    {
        builder.ToTable(
            "ProductFiles",
            tableBuilder => tableBuilder.HasCheckConstraint("CK_ProductFiles_SizeInBytes", "[SizeInBytes] > 0"));

        builder.HasKey(file => file.Id);

        builder.Property(file => file.OriginalName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(file => file.StoredName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(file => file.ContentType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(file => file.StoragePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(file => file.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(file => file.StoredName)
            .IsUnique();

        builder.HasOne(file => file.Product)
            .WithMany(product => product.Files)
            .HasForeignKey(file => file.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
