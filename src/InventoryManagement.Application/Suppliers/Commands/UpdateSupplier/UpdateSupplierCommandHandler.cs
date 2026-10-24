using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Suppliers.Dtos;
using InventoryManagement.Application.Suppliers.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateSupplierCommand, SupplierResponse>
{
    public async Task<SupplierResponse> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Supplier with id '{request.Id}' was not found.");

        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        if (await supplierRepository.ExistsByEmailAsync(normalizedEmail, request.Id, cancellationToken))
        {
            throw new ConflictException($"Supplier with email '{normalizedEmail}' already exists.");
        }

        supplier.Name = request.Name.Trim();
        supplier.ContactPerson = request.ContactPerson?.Trim();
        supplier.Email = normalizedEmail;
        supplier.PhoneNumber = request.PhoneNumber?.Trim();
        supplier.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<SupplierResponse>(supplier);
    }
}
