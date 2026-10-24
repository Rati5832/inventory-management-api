using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable(
            "StockMovements",
            tableBuilder =>
            {
                tableBuilder.HasCheckConstraint("CK_StockMovements_Quantity", "[Quantity] > 0");
                tableBuilder.HasCheckConstraint("CK_StockMovements_PreviousQuantity", "[PreviousQuantity] IS NULL OR [PreviousQuantity] >= 0");
                tableBuilder.HasCheckConstraint("CK_StockMovements_NewQuantity", "[NewQuantity] IS NULL OR [NewQuantity] >= 0");
            });

        builder.HasKey(movement => movement.Id);

        builder.Property(movement => movement.Type)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(movement => movement.Note)
            .HasMaxLength(500);

        builder.Property(movement => movement.CreatedAtUtc)
            .HasDefaultValueSql("SYSUTCDATETIME()");

        builder.HasOne(movement => movement.Stock)
            .WithMany(stock => stock.Movements)
            .HasForeignKey(movement => movement.StockId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
