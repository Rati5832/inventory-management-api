using InventoryManagement.Application.Categories.Dtos;
using InventoryManagement.Application.Categories.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQueryHandler(
    ICategoryRepository categoryRepository,
    IMapper mapper)
    : IRequestHandler<GetAllCategoriesQuery, IReadOnlyCollection<CategoryResponse>>
{
    public async Task<IReadOnlyCollection<CategoryResponse>> Handle(
        GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken);

        return mapper.Map<List<CategoryResponse>>(categories);
    }
}
