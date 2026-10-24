using InventoryManagement.Application.ProductSuppliers.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class ProductSupplierRepository(InventoryDbContext dbContext) : IProductSupplierRepository
{
    public Task<bool> ExistsAsync(Guid productId, Guid supplierId, CancellationToken cancellationToken = default)
    {
        return dbContext.ProductSuppliers.AnyAsync(
            productSupplier => productSupplier.ProductId == productId && productSupplier.SupplierId == supplierId,
            cancellationToken);
    }

    public async Task<IReadOnlyCollection<ProductSupplier>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await dbContext.ProductSuppliers
            .AsNoTracking()
            .Include(productSupplier => productSupplier.Supplier)
            .Where(productSupplier => productSupplier.ProductId == productId)
            .OrderBy(productSupplier => productSupplier.Supplier.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<ProductSupplier?> GetAsync(Guid productId, Guid supplierId, CancellationToken cancellationToken = default)
    {
        return dbContext.ProductSuppliers.SingleOrDefaultAsync(
            productSupplier => productSupplier.ProductId == productId && productSupplier.SupplierId == supplierId,
            cancellationToken);
    }

    public async Task AddAsync(ProductSupplier productSupplier, CancellationToken cancellationToken = default)
    {
        await dbContext.ProductSuppliers.AddAsync(productSupplier, cancellationToken);
    }

    public void Remove(ProductSupplier productSupplier)
    {
        dbContext.ProductSuppliers.Remove(productSupplier);
    }
}
