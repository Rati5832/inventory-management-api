using InventoryManagement.Application.ProductSuppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Queries.GetProductSuppliers;

public record GetProductSuppliersQuery(Guid ProductId) : IRequest<IReadOnlyCollection<ProductSupplierResponse>>;
