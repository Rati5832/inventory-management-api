namespace InventoryManagement.Application.Products.Dtos;

public record UpdateProductRequest(
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int ReorderLevel,
    bool IsActive,
    Guid CategoryId);
