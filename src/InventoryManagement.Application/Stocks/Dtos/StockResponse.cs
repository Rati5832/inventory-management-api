namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockResponse(
    Guid Id,
    Guid ProductId,
    string ProductName,
    string ProductSku,
    Guid WarehouseId,
    string WarehouseName,
    int Quantity,
    int ReorderLevel,
    bool IsLowStock,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);
