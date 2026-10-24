using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.ProductSuppliers.Interfaces;

public interface IProductSupplierRepository
{
    Task<bool> ExistsAsync(Guid productId, Guid supplierId, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<ProductSupplier>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<ProductSupplier?> GetAsync(Guid productId, Guid supplierId, CancellationToken cancellationToken = default);

    Task AddAsync(ProductSupplier productSupplier, CancellationToken cancellationToken = default);

    void Remove(ProductSupplier productSupplier);
}
