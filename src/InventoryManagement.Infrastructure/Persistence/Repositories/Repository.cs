using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public abstract class Repository<T>(InventoryDbContext dbContext) : IRepository<T>
    where T : BaseEntity
{
    protected InventoryDbContext DbContext { get; } = dbContext;

    protected DbSet<T> Entities { get; } = dbContext.Set<T>();

    public async Task<IReadOnlyCollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public void Remove(T entity)
    {
        Entities.Remove(entity);
    }
}
