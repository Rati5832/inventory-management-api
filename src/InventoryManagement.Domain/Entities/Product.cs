using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Sku { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int ReorderLevel { get; set; }

    public bool IsActive { get; set; } = true;

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

    public ICollection<ProductFile> Files { get; set; } = new List<ProductFile>();
}
