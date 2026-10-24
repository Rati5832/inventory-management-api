using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class StockRepository(InventoryDbContext dbContext) : Repository<Stock>(dbContext), IStockRepository
{
    public async Task<IReadOnlyCollection<Stock>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .Include(stock => stock.Product)
            .Include(stock => stock.Warehouse)
            .OrderBy(stock => stock.Product.Name)
            .ThenBy(stock => stock.Warehouse.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Stock?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Entities
            .AsNoTracking()
            .Include(stock => stock.Product)
            .Include(stock => stock.Warehouse)
            .SingleOrDefaultAsync(stock => stock.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Stock>> GetLowStocksAsync(CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .Include(stock => stock.Product)
            .Include(stock => stock.Warehouse)
            .Where(stock => stock.Quantity <= stock.Product.ReorderLevel)
            .OrderBy(stock => stock.Quantity)
            .ThenBy(stock => stock.Product.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Stock?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default)
    {
        return Entities.SingleOrDefaultAsync(
            stock => stock.ProductId == productId && stock.WarehouseId == warehouseId,
            cancellationToken);
    }
}
