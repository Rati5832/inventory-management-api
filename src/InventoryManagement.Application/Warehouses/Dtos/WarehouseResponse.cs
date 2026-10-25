namespace InventoryManagement.Application.Warehouses.Dtos;

public record WarehouseResponse(Guid Id, string Name, string Location, DateTime CreatedAtUtc);
