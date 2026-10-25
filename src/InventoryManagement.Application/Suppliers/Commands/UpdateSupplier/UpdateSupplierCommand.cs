using InventoryManagement.Application.Suppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.UpdateSupplier;

public record UpdateSupplierCommand(Guid Id, string Name, string? ContactPerson, string Email, string? PhoneNumber) : IRequest<SupplierResponse>;
