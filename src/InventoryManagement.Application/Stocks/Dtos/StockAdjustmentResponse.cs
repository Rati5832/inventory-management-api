namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockAdjustmentResponse(
    Guid MovementId,
    Guid StockId,
    int PreviousQuantity,
    int NewQuantity,
    int Difference,
    string Reason,
    DateTime CreatedAtUtc);
