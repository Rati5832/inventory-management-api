namespace InventoryManagement.Application.Stocks.Dtos;

public record StockOutRequest(Guid ProductId, Guid WarehouseId, int Quantity, string? Note);
