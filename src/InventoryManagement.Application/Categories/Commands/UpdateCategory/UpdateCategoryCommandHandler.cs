using InventoryManagement.Application.Categories.Dtos;
using InventoryManagement.Application.Categories.Interfaces;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandHandler(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Category with id '{request.Id}' was not found.");

        var normalizedName = request.Name.Trim();

        if (await categoryRepository.ExistsByNameAsync(
                normalizedName,
                request.Id,
                cancellationToken))
        {
            throw new ConflictException($"Category with name '{normalizedName}' already exists.");
        }

        category.Name = normalizedName;
        category.Description = request.Description?.Trim();
        category.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryResponse>(category);
    }
}
