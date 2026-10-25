namespace InventoryManagement.Application.Products.Dtos;

public record ProductResponse(
    Guid Id,
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int ReorderLevel,
    bool IsActive,
    Guid CategoryId,
    DateTime CreatedAtUtc);
