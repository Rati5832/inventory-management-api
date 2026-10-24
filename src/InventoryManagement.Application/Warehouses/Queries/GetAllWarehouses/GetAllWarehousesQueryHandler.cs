using InventoryManagement.Application.Warehouses.Dtos;
using InventoryManagement.Application.Warehouses.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Queries.GetAllWarehouses;

public class GetAllWarehousesQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper) : IRequestHandler<GetAllWarehousesQuery, IReadOnlyCollection<WarehouseResponse>>
{
    public async Task<IReadOnlyCollection<WarehouseResponse>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
    {
        var warehouses = await warehouseRepository.GetAllAsync(cancellationToken);

        return mapper.Map<List<WarehouseResponse>>(warehouses);
    }
}
