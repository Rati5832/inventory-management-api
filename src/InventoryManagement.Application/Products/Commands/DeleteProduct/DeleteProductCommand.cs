using MediatR;

namespace InventoryManagement.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;
