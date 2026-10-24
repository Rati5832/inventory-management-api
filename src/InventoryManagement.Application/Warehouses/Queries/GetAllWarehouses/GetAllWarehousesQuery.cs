using InventoryManagement.Application.Warehouses.Dtos;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Queries.GetAllWarehouses;

public sealed record GetAllWarehousesQuery : IRequest<IReadOnlyCollection<WarehouseResponse>>;
