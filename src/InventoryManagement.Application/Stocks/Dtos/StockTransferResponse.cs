namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record StockTransferResponse(
    Guid SourceMovementId,
    Guid DestinationMovementId,
    Guid SourceStockId,
    Guid DestinationStockId,
    Guid ProductId,
    Guid SourceWarehouseId,
    Guid DestinationWarehouseId,
    int Quantity,
    int SourceCurrentQuantity,
    int DestinationCurrentQuantity,
    string? Note);
