namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockInRequest(Guid ProductId, Guid WarehouseId, int Quantity, string? Note);
