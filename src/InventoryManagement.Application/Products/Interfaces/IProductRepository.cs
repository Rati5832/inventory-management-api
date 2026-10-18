using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Products.Models;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Products.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<bool> ExistsBySkuAsync(string sku, Guid? excludedProductId = null, CancellationToken cancellationToken = default);

    Task<bool> HasStocksAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<PagedResult<Product>> GetPagedAsync(ProductFilter filter, CancellationToken cancellationToken = default);
}
