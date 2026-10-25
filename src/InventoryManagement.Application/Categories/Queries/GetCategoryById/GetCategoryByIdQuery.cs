using InventoryManagement.Application.Categories.Dtos;
using MediatR;

namespace InventoryManagement.Application.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponse>;
