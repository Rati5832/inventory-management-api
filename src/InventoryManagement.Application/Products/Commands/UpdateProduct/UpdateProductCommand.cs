using InventoryManagement.Application.Products.Dtos;
using MediatR;

namespace InventoryManagement.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Sku,
    string? Description,
    decimal Price,
    int ReorderLevel,
    bool IsActive,
    Guid CategoryId) : IRequest<ProductResponse>;
