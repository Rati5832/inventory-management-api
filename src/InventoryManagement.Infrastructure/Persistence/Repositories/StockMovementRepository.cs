using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class StockMovementRepository(InventoryDbContext dbContext) : Repository<StockMovement>(dbContext), IStockMovementRepository
{
    public async Task<IReadOnlyCollection<StockMovement>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .Include(movement => movement.Stock)
                .ThenInclude(stock => stock.Product)
            .Include(movement => movement.Stock)
                .ThenInclude(stock => stock.Warehouse)
            .OrderByDescending(movement => movement.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<StockMovement>> GetByStockIdAsync(Guid stockId, CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .Where(movement => movement.StockId == stockId)
            .OrderByDescending(movement => movement.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }
}
