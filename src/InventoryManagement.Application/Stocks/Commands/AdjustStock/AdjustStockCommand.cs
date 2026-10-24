using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.AdjustStock;

public sealed record AdjustStockCommand(Guid StockId, int NewQuantity, string Reason) : IRequest<StockAdjustmentResponse>;
