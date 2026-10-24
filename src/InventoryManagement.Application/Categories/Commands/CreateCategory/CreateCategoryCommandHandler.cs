using InventoryManagement.Application.Categories.Dtos;
using InventoryManagement.Application.Categories.Interfaces;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCategoryCommand, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var normalizedName = request.Name.Trim();

        if (await categoryRepository.ExistsByNameAsync(normalizedName, cancellationToken: cancellationToken))
        {
            throw new ConflictException($"Category with name '{normalizedName}' already exists.");
        }

        var category = mapper.Map<Category>(request);
        category.Name = normalizedName;
        category.Description = request.Description?.Trim();

        await categoryRepository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CategoryResponse>(category);
    }
}
