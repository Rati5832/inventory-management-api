using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.DeleteSupplier;

public record DeleteSupplierCommand(Guid Id) : IRequest;
