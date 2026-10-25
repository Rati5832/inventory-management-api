using InventoryManagement.Application.ProductFiles.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class ProductFileRepository(InventoryDbContext dbContext) : Repository<ProductFile>(dbContext), IProductFileRepository
{
    public async Task<IReadOnlyCollection<ProductFile>> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .Where(file => file.ProductId == productId)
            .OrderByDescending(file => file.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public Task<ProductFile?> GetByProductAndIdAsync(Guid productId, Guid fileId, CancellationToken cancellationToken = default)
    {
        return Entities.AsNoTracking().SingleOrDefaultAsync(
            file => file.ProductId == productId && file.Id == fileId,
            cancellationToken);
    }
}
