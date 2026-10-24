namespace InventoryManagement.Application.Suppliers.Dtos;

public sealed record CreateSupplierRequest(string Name, string? ContactPerson, string Email, string? PhoneNumber);
