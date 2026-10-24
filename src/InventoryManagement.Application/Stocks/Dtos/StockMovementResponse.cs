using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockMovementResponse(
    Guid Id,
    Guid StockId,
    Guid ProductId,
    Guid WarehouseId,
    StockMovementType Type,
    int Quantity,
    int CurrentQuantity,
    string? Note,
    DateTime CreatedAtUtc);
