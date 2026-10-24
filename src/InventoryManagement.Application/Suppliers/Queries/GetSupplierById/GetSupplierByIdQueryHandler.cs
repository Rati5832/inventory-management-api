using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Suppliers.Dtos;
using InventoryManagement.Application.Suppliers.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQueryHandler(ISupplierRepository supplierRepository, IMapper mapper) : IRequestHandler<GetSupplierByIdQuery, SupplierResponse>
{
    public async Task<SupplierResponse> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await supplierRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Supplier with id '{request.Id}' was not found.");

        return mapper.Map<SupplierResponse>(supplier);
    }
}
