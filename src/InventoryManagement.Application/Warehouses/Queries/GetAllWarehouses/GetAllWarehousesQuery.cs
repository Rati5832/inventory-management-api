using InventoryManagement.Application.Warehouses.Dtos;
using MediatR;

namespace InventoryManagement.Application.Warehouses.Queries.GetAllWarehouses;

public record GetAllWarehousesQuery : IRequest<IReadOnlyCollection<WarehouseResponse>>;
