using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStockMovements;

public sealed record GetAllStockMovementsQuery : IRequest<IReadOnlyCollection<StockMovementDetailsResponse>>;
