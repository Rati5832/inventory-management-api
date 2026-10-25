using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Commands.RemoveSupplier;

public record RemoveSupplierCommand(Guid ProductId, Guid SupplierId) : IRequest;
