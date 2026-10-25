using InventoryManagement.Application.Warehouses.Dtos;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Commands.CreateWarehouse;

public record CreateWarehouseCommand(string Name, string Location) : IRequest<WarehouseResponse>;
