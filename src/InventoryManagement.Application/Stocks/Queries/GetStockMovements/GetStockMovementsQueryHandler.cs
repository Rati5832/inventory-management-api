using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetStockMovements;

public class GetStockMovementsQueryHandler(IStockRepository stockRepository, IStockMovementRepository stockMovementRepository) : IRequestHandler<GetStockMovementsQuery, IReadOnlyCollection<StockMovementHistoryResponse>>
{
    public async Task<IReadOnlyCollection<StockMovementHistoryResponse>> Handle(GetStockMovementsQuery request, CancellationToken cancellationToken)
    {
        if (await stockRepository.GetByIdAsync(request.StockId, cancellationToken) is null)
        {
            throw new NotFoundException($"Stock with id '{request.StockId}' was not found.");
        }

        var movements = await stockMovementRepository.GetByStockIdAsync(request.StockId, cancellationToken);

        return movements.Select(movement => new StockMovementHistoryResponse(
            movement.Id,
            movement.Type,
            movement.Quantity,
            movement.PreviousQuantity,
            movement.NewQuantity,
            movement.Note,
            movement.CreatedAtUtc)).ToList();
    }
}
