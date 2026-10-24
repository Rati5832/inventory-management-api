using InventoryManagement.Application.Warehouses.Dtos;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Queries.GetWarehouseById;

public sealed record GetWarehouseByIdQuery(Guid Id) : IRequest<WarehouseResponse>;
