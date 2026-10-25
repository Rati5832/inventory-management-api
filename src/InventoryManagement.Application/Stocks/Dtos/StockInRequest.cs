namespace InventoryManagement.Application.Stocks.Dtos;

public record StockInRequest(Guid ProductId, Guid WarehouseId, int Quantity, string? Note);
