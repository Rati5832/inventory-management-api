namespace InventoryManagement.Application.Suppliers.Dtos;

public record SupplierResponse(Guid Id, string Name, string? ContactPerson, string Email, string? PhoneNumber, DateTime CreatedAtUtc);
