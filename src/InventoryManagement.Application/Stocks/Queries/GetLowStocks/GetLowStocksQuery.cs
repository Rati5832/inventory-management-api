using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetLowStocks;

public record GetLowStocksQuery : IRequest<IReadOnlyCollection<StockResponse>>;
