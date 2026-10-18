namespace InventoryManagement.Application.Products.Models;

public sealed record ProductFilter(
    int PageNumber,
    int PageSize,
    string? Search,
    Guid? CategoryId,
    bool? IsActive,
    decimal? MinPrice,
    decimal? MaxPrice,
    string SortBy,
    string SortDirection);
