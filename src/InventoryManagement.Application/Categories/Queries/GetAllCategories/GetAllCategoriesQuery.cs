using InventoryManagement.Application.Categories.Dtos;
using MediatR;

namespace InventoryManagement.Application.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery : IRequest<IReadOnlyCollection<CategoryResponse>>;
