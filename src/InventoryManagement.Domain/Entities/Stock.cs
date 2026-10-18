using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Stock : BaseEntity
{
    public int Quantity { get; set; }

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;

    public Guid WarehouseId { get; set; }

    public Warehouse Warehouse { get; set; } = null!;

    public ICollection<StockMovement> Movements { get; set; } = new List<StockMovement>();
}
