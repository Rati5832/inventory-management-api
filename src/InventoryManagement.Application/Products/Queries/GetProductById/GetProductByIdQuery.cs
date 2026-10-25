using InventoryManagement.Application.Products.Dtos;
using MediatR;

namespace InventoryManagement.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;
