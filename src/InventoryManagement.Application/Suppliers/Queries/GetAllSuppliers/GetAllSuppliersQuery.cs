using InventoryManagement.Application.Suppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Queries.GetAllSuppliers;

public record GetAllSuppliersQuery : IRequest<IReadOnlyCollection<SupplierResponse>>;
