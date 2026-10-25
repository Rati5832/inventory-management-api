using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Application.ProductFiles.Interfaces;

public interface IProductFileRepository : IRepository<ProductFile>
{
    Task<IReadOnlyCollection<ProductFile>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<ProductFile?> GetByProductAndIdAsync(Guid productId, Guid fileId, CancellationToken cancellationToken = default);
}
