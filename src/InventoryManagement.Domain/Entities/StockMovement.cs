using InventoryManagement.Domain.Common;
using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Domain.Entities;

public class StockMovement : BaseEntity
{
    public StockMovementType Type { get; set; }

    public int Quantity { get; set; }

    public string? Note { get; set; }

    public Guid StockId { get; set; }

    public Stock Stock { get; set; } = null!;
}
