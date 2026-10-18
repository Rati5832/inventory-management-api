using InventoryManagement.Application.Categories.Dtos;
using MediatR;

namespace InventoryManagement.Application.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(string Name, string? Description) : IRequest<CategoryResponse>;
