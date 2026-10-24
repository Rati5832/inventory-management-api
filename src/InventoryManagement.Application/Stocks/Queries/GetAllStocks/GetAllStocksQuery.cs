using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetAllStocks;

public sealed record GetAllStocksQuery : IRequest<IReadOnlyCollection<StockResponse>>;
