using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.ProductSuppliers.Interfaces;
using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Commands.RemoveSupplier;

public class RemoveSupplierCommandHandler(IProductSupplierRepository productSupplierRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveSupplierCommand>
{
    public async Task Handle(RemoveSupplierCommand request, CancellationToken cancellationToken)
    {
        var productSupplier = await productSupplierRepository.GetAsync(request.ProductId, request.SupplierId, cancellationToken)
            ?? throw new NotFoundException("The supplier is not assigned to this product.");

        productSupplierRepository.Remove(productSupplier);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
