using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.ToTable(
            "Stocks",
            tableBuilder => tableBuilder.HasCheckConstraint("CK_Stocks_Quantity", "[Quantity] >= 0"));

        builder.HasKey(stock => stock.Id);

        builder.Property(stock => stock.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasIndex(stock => new { stock.ProductId, stock.WarehouseId })
            .IsUnique();

        builder.HasOne(stock => stock.Product)
            .WithMany(product => product.Stocks)
            .HasForeignKey(stock => stock.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(stock => stock.Warehouse)
            .WithMany(warehouse => warehouse.Stocks)
            .HasForeignKey(stock => stock.WarehouseId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
