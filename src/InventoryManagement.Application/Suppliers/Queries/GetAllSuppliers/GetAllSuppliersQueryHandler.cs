using InventoryManagement.Application.Suppliers.Dtos;
using InventoryManagement.Application.Suppliers.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Queries.GetAllSuppliers;

public class GetAllSuppliersQueryHandler(ISupplierRepository supplierRepository, IMapper mapper) : IRequestHandler<GetAllSuppliersQuery, IReadOnlyCollection<SupplierResponse>>
{
    public async Task<IReadOnlyCollection<SupplierResponse>> Handle(GetAllSuppliersQuery request, CancellationToken cancellationToken)
    {
        var suppliers = await supplierRepository.GetAllAsync(cancellationToken);

        return mapper.Map<List<SupplierResponse>>(suppliers);
    }
}
