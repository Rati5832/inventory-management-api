using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Commands.RemoveSupplier;

public sealed record RemoveSupplierCommand(Guid ProductId, Guid SupplierId) : IRequest;
