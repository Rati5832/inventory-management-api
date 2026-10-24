using InventoryManagement.Domain.Enums;

namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockMovementHistoryResponse(
    Guid Id,
    StockMovementType Type,
    int Quantity,
    int? PreviousQuantity,
    int? NewQuantity,
    string? Note,
    DateTime CreatedAtUtc);
