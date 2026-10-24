using InventoryManagement.Application.Categories.Dtos;
using InventoryManagement.Application.Categories.Interfaces;
using InventoryManagement.Application.Common.Exceptions;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Categories.Queries.GetCategoryById;

public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryResponse>
{
    public async Task<CategoryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Category with id '{request.Id}' was not found.");

        return mapper.Map<CategoryResponse>(category);
    }
}
