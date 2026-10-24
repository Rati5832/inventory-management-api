using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetLowStocks;

public class GetLowStocksQueryHandler(IStockRepository stockRepository) : IRequestHandler<GetLowStocksQuery, IReadOnlyCollection<StockResponse>>
{
    public async Task<IReadOnlyCollection<StockResponse>> Handle(GetLowStocksQuery request, CancellationToken cancellationToken)
    {
        var stocks = await stockRepository.GetLowStocksAsync(cancellationToken);

        return stocks.Select(stock => new StockResponse(
            stock.Id,
            stock.ProductId,
            stock.Product.Name,
            stock.Product.Sku,
            stock.WarehouseId,
            stock.Warehouse.Name,
            stock.Quantity,
            stock.Product.ReorderLevel,
            true,
            stock.CreatedAtUtc,
            stock.UpdatedAtUtc)).ToList();
    }
}
