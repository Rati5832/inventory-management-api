using InventoryManagement.Application.Categories.Dtos;
using MediatR;

namespace InventoryManagement.Application.Categories.Commands.UpdateCategory;

public record UpdateCategoryCommand(
    Guid Id,
    string Name,
    string? Description) : IRequest<CategoryResponse>;
