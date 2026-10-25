using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.TransferStock;

public record TransferStockCommand(
    Guid ProductId,
    Guid SourceWarehouseId,
    Guid DestinationWarehouseId,
    int Quantity,
    string? Note) : IRequest<StockTransferResponse>;
