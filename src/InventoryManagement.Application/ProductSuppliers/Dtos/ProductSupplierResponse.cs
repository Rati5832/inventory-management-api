namespace InventoryManagement.Application.ProductSuppliers.Dtos;

public sealed record ProductSupplierResponse(Guid ProductId, string ProductName, Guid SupplierId, string SupplierName, string SupplierEmail);
