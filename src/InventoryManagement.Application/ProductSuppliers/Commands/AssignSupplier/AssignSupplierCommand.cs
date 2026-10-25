using InventoryManagement.Application.ProductSuppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Commands.AssignSupplier;

public record AssignSupplierCommand(Guid ProductId, Guid SupplierId) : IRequest<ProductSupplierResponse>;
