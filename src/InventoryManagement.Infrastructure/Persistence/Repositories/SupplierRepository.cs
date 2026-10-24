using InventoryManagement.Application.Suppliers.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class SupplierRepository(InventoryDbContext dbContext) : Repository<Supplier>(dbContext), ISupplierRepository
{
    public Task<bool> ExistsByEmailAsync(string email, Guid? excludedSupplierId = null, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(
            supplier => supplier.Email == email && (!excludedSupplierId.HasValue || supplier.Id != excludedSupplierId.Value),
            cancellationToken);
    }

    public Task<bool> HasProductsAsync(Guid supplierId, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(supplier => supplier.Id == supplierId && supplier.ProductSuppliers.Any(), cancellationToken);
    }
}
