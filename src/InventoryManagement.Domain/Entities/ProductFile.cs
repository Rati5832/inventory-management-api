using InventoryManagement.Domain.Common;

namespace InventoryManagement.Domain.Entities;

public class ProductFile : BaseEntity
{
    public string OriginalName { get; set; } = string.Empty;

    public string StoredName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long SizeInBytes { get; set; }

    public string StoragePath { get; set; } = string.Empty;

    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;
}
