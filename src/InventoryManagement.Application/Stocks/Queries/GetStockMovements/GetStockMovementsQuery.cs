using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetStockMovements;

public sealed record GetStockMovementsQuery(Guid StockId) : IRequest<IReadOnlyCollection<StockMovementHistoryResponse>>;
