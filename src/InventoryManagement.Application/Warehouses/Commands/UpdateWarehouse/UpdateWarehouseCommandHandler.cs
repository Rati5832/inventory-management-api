using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Warehouses.Dtos;
using InventoryManagement.Application.Warehouses.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.UpdateWarehouse;

public class UpdateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateWarehouseCommand, WarehouseResponse>
{
    public async Task<WarehouseResponse> Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Warehouse with id '{request.Id}' was not found.");

        var normalizedName = request.Name.Trim();

        if (await warehouseRepository.ExistsByNameAsync(normalizedName, request.Id, cancellationToken))
        {
            throw new ConflictException($"Warehouse with name '{normalizedName}' already exists.");
        }

        warehouse.Name = normalizedName;
        warehouse.Location = request.Location.Trim();
        warehouse.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<WarehouseResponse>(warehouse);
    }
}
