using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStocks;

public class GetAllStocksQueryHandler(IStockRepository stockRepository) : IRequestHandler<GetAllStocksQuery, IReadOnlyCollection<StockResponse>>
{
    public async Task<IReadOnlyCollection<StockResponse>> Handle(GetAllStocksQuery request, CancellationToken cancellationToken)
    {
        var stocks = await stockRepository.GetAllWithDetailsAsync(cancellationToken);

        return stocks.Select(stock => new StockResponse(
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
            stock.UpdatedAtUtc)).ToList();
    }
}
