namespace InventoryManagement.Application.Suppliers.Dtos;

public sealed record UpdateSupplierRequest(string Name, string? ContactPerson, string Email, string? PhoneNumber);
