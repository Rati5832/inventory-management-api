namespace InventoryManagement.Application.Stocks.Dtos;

public record AdjustStockRequest(int NewQuantity, string Reason);
