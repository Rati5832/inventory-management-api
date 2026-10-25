using InventoryManagement.Application.Suppliers.Dtos;
using MediatR;

namespace InventoryManagement.Application.Suppliers.Queries.GetSupplierById;

public record GetSupplierByIdQuery(Guid Id) : IRequest<SupplierResponse>;
