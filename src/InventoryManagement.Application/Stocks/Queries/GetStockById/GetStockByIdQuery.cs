using InventoryManagement.Application.Stocks.Dtos;
using MediatR;

namespace InventoryManagement.Application.Stocks.Queries.GetStockById;

public record GetStockByIdQuery(Guid Id) : IRequest<StockResponse>;
