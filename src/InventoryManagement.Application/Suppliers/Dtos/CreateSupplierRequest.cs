namespace InventoryManagement.Application.Suppliers.Dtos;

public record CreateSupplierRequest(string Name, string? ContactPerson, string Email, string? PhoneNumber);
