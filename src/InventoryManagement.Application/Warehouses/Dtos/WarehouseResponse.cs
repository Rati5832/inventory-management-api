namespace InventoryManagement.Application.Warehouses.Dtos;

public sealed record WarehouseResponse(Guid Id, string Name, string Location, DateTime CreatedAtUtc);
