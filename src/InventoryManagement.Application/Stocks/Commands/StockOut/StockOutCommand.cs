using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.StockOut;

public sealed record StockOutCommand(Guid ProductId, Guid WarehouseId, int Quantity, string? Note) : IRequest<StockMovementResponse>;
