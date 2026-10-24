using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStockMovements;

public class GetAllStockMovementsQueryHandler(IStockMovementRepository stockMovementRepository) : IRequestHandler<GetAllStockMovementsQuery, IReadOnlyCollection<StockMovementDetailsResponse>>
{
    public async Task<IReadOnlyCollection<StockMovementDetailsResponse>> Handle(GetAllStockMovementsQuery request, CancellationToken cancellationToken)
    {
        var movements = await stockMovementRepository.GetAllWithDetailsAsync(cancellationToken);

        return movements.Select(movement => new StockMovementDetailsResponse(
            movement.Id,
            movement.StockId,
            movement.Stock.ProductId,
            movement.Stock.Product.Name,
            movement.Stock.Product.Sku,
            movement.Stock.WarehouseId,
            movement.Stock.Warehouse.Name,
            movement.Type,
            movement.Quantity,
            movement.PreviousQuantity,
            movement.NewQuantity,
            movement.Note,
            movement.CreatedAtUtc)).ToList();
    }
}
