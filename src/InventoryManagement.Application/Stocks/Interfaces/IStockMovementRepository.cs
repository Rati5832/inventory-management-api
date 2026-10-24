using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Stocks.Interfaces;

public interface IStockMovementRepository : IRepository<StockMovement>
{
    Task<IReadOnlyCollection<StockMovement>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<StockMovement>> GetByStockIdAsync(Guid stockId, CancellationToken cancellationToken = default);
}
