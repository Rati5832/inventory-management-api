using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.StockIn;

public sealed record StockInCommand(Guid ProductId, Guid WarehouseId, int Quantity, string? Note) : IRequest<StockMovementResponse>;
