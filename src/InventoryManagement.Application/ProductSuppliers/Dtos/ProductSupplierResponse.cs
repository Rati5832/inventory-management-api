namespace InventoryManagement.Application.ProductSuppliers.Dtos;

public record ProductSupplierResponse(Guid ProductId, string ProductName, Guid SupplierId, string SupplierName, string SupplierEmail);
