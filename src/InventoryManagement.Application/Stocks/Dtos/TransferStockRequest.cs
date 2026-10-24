namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record TransferStockRequest(Guid ProductId, Guid SourceWarehouseId, Guid DestinationWarehouseId, int Quantity, string? Note);
