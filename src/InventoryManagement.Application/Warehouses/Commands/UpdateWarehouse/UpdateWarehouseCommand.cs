using InventoryManagement.Application.Warehouses.Dtos;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.UpdateWarehouse;

public record UpdateWarehouseCommand(Guid Id, string Name, string Location) : IRequest<WarehouseResponse>;
