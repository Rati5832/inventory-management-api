using InventoryManagement.Application.Warehouses.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class WarehouseRepository(InventoryDbContext dbContext) : Repository<Warehouse>(dbContext), IWarehouseRepository
{
    public Task<bool> ExistsByNameAsync(string name, Guid? excludedWarehouseId = null, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(
            warehouse => warehouse.Name == name && (!excludedWarehouseId.HasValue || warehouse.Id != excludedWarehouseId.Value),
            cancellationToken);
    }

    public Task<bool> HasStocksAsync(Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(warehouse => warehouse.Id == warehouseId && warehouse.Stocks.Any(), cancellationToken);
    }
}
