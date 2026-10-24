using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Warehouses.Dtos;
using InventoryManagement.Application.Warehouses.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Queries.GetWarehouseById;

public class GetWarehouseByIdQueryHandler(IWarehouseRepository warehouseRepository, IMapper mapper) : IRequestHandler<GetWarehouseByIdQuery, WarehouseResponse>
{
    public async Task<WarehouseResponse> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Warehouse with id '{request.Id}' was not found.");

        return mapper.Map<WarehouseResponse>(warehouse);
    }
}
