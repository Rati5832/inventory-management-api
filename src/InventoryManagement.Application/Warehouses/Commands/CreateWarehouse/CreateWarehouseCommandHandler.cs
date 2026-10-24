using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Warehouses.Dtos;
using InventoryManagement.Application.Warehouses.Interfaces;
using InventoryManagement.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateWarehouseCommand, WarehouseResponse>
{
    public async Task<WarehouseResponse> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var normalizedName = request.Name.Trim();

        if (await warehouseRepository.ExistsByNameAsync(normalizedName, cancellationToken: cancellationToken))
        {
            throw new ConflictException($"Warehouse with name '{normalizedName}' already exists.");
        }

        var warehouse = mapper.Map<Warehouse>(request);
        warehouse.Name = normalizedName;
        warehouse.Location = request.Location.Trim();

        await warehouseRepository.AddAsync(warehouse, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<WarehouseResponse>(warehouse);
    }
}
