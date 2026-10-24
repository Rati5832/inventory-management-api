using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Product with id '{request.Id}' was not found.");

        if (await productRepository.HasStocksAsync(request.Id, cancellationToken))
        {
            throw new ConflictException($"Product with id '{request.Id}' cannot be deleted because it has stock records.");
        }

        productRepository.Remove(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
