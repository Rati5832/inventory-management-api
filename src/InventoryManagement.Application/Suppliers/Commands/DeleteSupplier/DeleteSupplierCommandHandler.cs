using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Suppliers.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteSupplierCommand>
{
    public async Task Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Supplier with id '{request.Id}' was not found.");

        if (await supplierRepository.HasProductsAsync(request.Id, cancellationToken))
        {
            throw new ConflictException($"Supplier with id '{request.Id}' cannot be deleted because it is linked to products.");
        }

        supplierRepository.Remove(supplier);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
