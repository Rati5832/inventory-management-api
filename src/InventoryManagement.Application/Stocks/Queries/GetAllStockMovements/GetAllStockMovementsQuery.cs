using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStockMovements;

public record GetAllStockMovementsQuery : IRequest<IReadOnlyCollection<StockMovementDetailsResponse>>;
