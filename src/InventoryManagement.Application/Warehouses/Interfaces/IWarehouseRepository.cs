using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Warehouses.Interfaces;

public interface IWarehouseRepository : IRepository<Warehouse>
{
    Task<bool> ExistsByNameAsync(string name, Guid? excludedWarehouseId = null, CancellationToken cancellationToken = default);

    Task<bool> HasStocksAsync(Guid warehouseId, CancellationToken cancellationToken = default);
}
