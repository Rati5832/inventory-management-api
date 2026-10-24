using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetStockById;

public sealed record GetStockByIdQuery(Guid Id) : IRequest<StockResponse>;
