using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.DeleteWarehouse;

public record DeleteWarehouseCommand(Guid Id) : IRequest;
