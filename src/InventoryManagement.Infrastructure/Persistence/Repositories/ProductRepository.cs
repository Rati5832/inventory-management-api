using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.Products.Models;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class ProductRepository(InventoryDbContext dbContext) : Repository<Product>(dbContext), IProductRepository
{
    public Task<bool> ExistsBySkuAsync(string sku, Guid? excludedProductId = null, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(
            product => product.Sku == sku && (!excludedProductId.HasValue || product.Id != excludedProductId.Value),
            cancellationToken);
    }

    public async Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter, CancellationToken cancellationToken = default)
    {
        var query = Entities.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.Trim();
            query = query.Where(product => product.Name.Contains(search) || product.Sku.Contains(search));
        }

        if (filter.CategoryId.HasValue)
        {
            query = query.Where(product => product.CategoryId == filter.CategoryId.Value);
        }

        if (filter.IsActive.HasValue)
        {
            query = query.Where(product => product.IsActive == filter.IsActive.Value);
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(product => product.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(product => product.Price <= filter.MaxPrice.Value);
        }

        query = (filter.SortBy.ToLowerInvariant(), filter.SortDirection.ToLowerInvariant()) switch
        {
            ("name", "desc") => query.OrderByDescending(product => product.Name),
            ("price", "asc") => query.OrderBy(product => product.Price),
            ("price", "desc") => query.OrderByDescending(product => product.Price),
            _ => query.OrderBy(product => product.Name)
        };

        var totalCount = await query.CountAsync(cancellationToken);
        var products = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Product>(products, filter.PageNumber, filter.PageSize, totalCount);
    }

    public Task<bool> HasStocksAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(product => product.Id == productId && product.Stocks.Any(), cancellationToken);
    }
}
