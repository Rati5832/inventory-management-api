using InventoryManagement.Application.Suppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Queries.GetAllSuppliers;

public sealed record GetAllSuppliersQuery : IRequest<IReadOnlyCollection<SupplierResponse>>;
