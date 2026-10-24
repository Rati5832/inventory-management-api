using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockMovementDetailsResponse(
    Guid Id,
    Guid StockId,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    Guid WarehouseId,
    string WarehouseName,
    StockMovementType Type,
    int Quantity,
    int? PreviousQuantity,
    int? NewQuantity,
    string? Note,
    DateTime CreatedAtUtc);
