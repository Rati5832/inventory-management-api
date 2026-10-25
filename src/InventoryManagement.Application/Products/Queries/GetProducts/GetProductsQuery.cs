using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Products.Dtos;
using MediatR;

namespace InventoryManagement.Application.Products.Queries.GetProducts;

public record GetProductsQuery(
    int PageNumber,
    int PageSize,
    string? Search,
    Guid? CategoryId,
    bool? IsActive,
    decimal? MinPrice,
    decimal? MaxPrice,
    string SortBy,
    string SortDirection) : IRequest<PagedResult<ProductResponse>>;
