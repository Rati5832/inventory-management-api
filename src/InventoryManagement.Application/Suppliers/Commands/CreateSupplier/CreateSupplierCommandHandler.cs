using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Suppliers.Dtos;
using InventoryManagement.Application.Suppliers.Interfaces;
using InventoryManagement.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.CreateSupplier;

public class CreateSupplierCommandHandler(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateSupplierCommand, SupplierResponse>
{
    public async Task<SupplierResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        if (await supplierRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken: cancellationToken))
        {
            throw new ConflictException($"Supplier with email '{normalizedEmail}' already exists.");
        }

        var supplier = mapper.Map<Supplier>(request);
        supplier.Name = request.Name.Trim();
        supplier.ContactPerson = request.ContactPerson?.Trim();
        supplier.Email = normalizedEmail;
        supplier.PhoneNumber = request.PhoneNumber?.Trim();

        await supplierRepository.AddAsync(supplier, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<SupplierResponse>(supplier);
    }
}
