using InventoryManagement.Application.Categories.Interfaces;
using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Persistence.Repositories;

public class CategoryRepository(InventoryDbContext dbContext) : Repository<Category>(dbContext), ICategoryRepository
{
    public Task<bool> ExistsByNameAsync(string name, Guid? excludedCategoryId = null, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(
            category => category.Name == name && (!excludedCategoryId.HasValue || category.Id != excludedCategoryId.Value),
            cancellationToken);
    }

    public Task<bool> HasProductsAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return Entities.AnyAsync(category => category.Id == categoryId && category.Products.Any(), cancellationToken);
    }
}
