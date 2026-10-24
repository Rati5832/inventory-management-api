namespace InventoryManagement.Application.Stocks.Dtos;

public sealed record AdjustStockRequest(int NewQuantity, string Reason);
