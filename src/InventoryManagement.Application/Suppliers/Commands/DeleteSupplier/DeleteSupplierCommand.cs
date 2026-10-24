using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.DeleteSupplier;

public sealed record DeleteSupplierCommand(Guid Id) : IRequest;
