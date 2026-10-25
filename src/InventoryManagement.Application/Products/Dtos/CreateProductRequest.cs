namespace InventoryManagement.Application.Products.Dtos;

public record CreateProductRequest(
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int ReorderLevel,
    Guid CategoryId);
