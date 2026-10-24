using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Warehouses.Interfaces;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.DeleteWarehouse;

public class DeleteWarehouseCommandHandler(IWarehouseRepository warehouseRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteWarehouseCommand>
{
    public async Task Handle(DeleteWarehouseCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Warehouse with id '{request.Id}' was not found.");

        if (await warehouseRepository.HasStocksAsync(request.Id, cancellationToken))
        {
            throw new ConflictException($"Warehouse with id '{request.Id}' cannot be deleted because it has stock records.");
        }

        warehouseRepository.Remove(warehouse);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
