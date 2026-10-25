using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetStockMovements;

public record GetStockMovementsQuery(Guid StockId) : IRequest<IReadOnlyCollection<StockMovementHistoryResponse>>;
