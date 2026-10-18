using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(supplier => supplier.Id);

        builder.Property(supplier => supplier.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(supplier => supplier.ContactPerson)
            .HasMaxLength(100);

        builder.Property(supplier => supplier.Email)
            .IsRequired()
            .HasMaxLength(254);

        builder.Property(supplier => supplier.PhoneNumber)
            .HasMaxLength(30);

        builder.Property(supplier => supplier.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(supplier => supplier.Email)
            .IsUnique();
    }
}
