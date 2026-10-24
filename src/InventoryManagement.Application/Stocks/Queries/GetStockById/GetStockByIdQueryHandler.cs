using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetStockById;

public class GetStockByIdQueryHandler(IStockRepository stockRepository) : IRequestHandler<GetStockByIdQuery, StockResponse>
{
    public async Task<StockResponse> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
    {
        var stock = await stockRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Stock with id '{request.Id}' was not found.");

        return new StockResponse(
            stock.Id,
            stock.ProductId,
            stock.Product.Name,
            stock.Product.Sku,
            stock.WarehouseId,
            stock.Warehouse.Name,
            stock.Quantity,
            stock.Product.ReorderLevel,
            stock.Quantity <= stock.Product.ReorderLevel,
            stock.CreatedAtUtc,
            stock.UpdatedAtUtc);
    }
}
