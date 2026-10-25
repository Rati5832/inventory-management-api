namespace InventoryManagement.Application.Suppliers.Dtos;

public record UpdateSupplierRequest(string Name, string? ContactPerson, string Email, string? PhoneNumber);
