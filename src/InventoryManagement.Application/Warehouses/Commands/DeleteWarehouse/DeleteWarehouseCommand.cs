using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.DeleteWarehouse;

public sealed record DeleteWarehouseCommand(Guid Id) : IRequest;
