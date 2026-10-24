using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.Stocks.Interfaces;

public interface IStockRepository : IRepository<Stock>
{
    Task<IReadOnlyCollection<Stock>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);

    Task<Stock?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<Stock>> GetLowStocksAsync(CancellationToken cancellationToken = default);

    Task<Stock?> GetByProductAndWarehouseAsync(Guid productId, Guid warehouseId, CancellationToken cancellationToken = default);
}
