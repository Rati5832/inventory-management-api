namespace InventoryManagement.Application.Suppliers.Dtos;

public sealed record SupplierResponse(Guid Id, string Name, string? ContactPerson, string Email, string? PhoneNumber, DateTime CreatedAtUtc);
