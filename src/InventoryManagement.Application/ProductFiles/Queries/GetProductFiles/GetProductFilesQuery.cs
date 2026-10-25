using InventoryManagement.Application.ProductFiles.Dtos;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Queries.GetProductFiles;

public record GetProductFilesQuery(Guid ProductId) : IRequest<IReadOnlyCollection<ProductFileResponse>>;
