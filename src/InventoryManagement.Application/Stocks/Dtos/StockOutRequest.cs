namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockOutRequest(Guid ProductId, Guid WarehouseId, int Quantity, string? Note);
