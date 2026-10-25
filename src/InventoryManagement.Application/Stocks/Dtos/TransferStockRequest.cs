namespace InventoryManagement.Application.Stocks.Dtos;

public record TransferStockRequest(Guid ProductId, Guid SourceWarehouseId, Guid DestinationWarehouseId, int Quantity, string? Note);
