using InventoryManagement.Application.ProductSuppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Commands.AssignSupplier;

public sealed record AssignSupplierCommand(Guid ProductId, Guid SupplierId) : IRequest<ProductSupplierResponse>;
