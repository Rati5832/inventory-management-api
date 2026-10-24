using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Suppliers.Interfaces;

public interface ISupplierRepository : IRepository<Supplier>
{
    Task<bool> ExistsByEmailAsync(string email, Guid? excludedSupplierId = null, CancellationToken cancellationToken = default);

    Task<bool> HasProductsAsync(Guid supplierId, CancellationToken cancellationToken = default);
}
