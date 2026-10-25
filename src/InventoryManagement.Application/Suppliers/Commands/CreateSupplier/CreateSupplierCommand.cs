using InventoryManagement.Application.Suppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Commands.CreateSupplier;

public record CreateSupplierCommand(string Name, string? ContactPerson, string Email, string? PhoneNumber) : IRequest<SupplierResponse>;
