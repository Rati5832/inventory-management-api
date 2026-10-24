using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.ProductSuppliers.Dtos;
using InventoryManagement.Application.ProductSuppliers.Interfaces;
using InventoryManagement.Application.Suppliers.Interfaces;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Commands.AssignSupplier;

public class AssignSupplierCommandHandler(
    IProductSupplierRepository productSupplierRepository,
    IProductRepository productRepository,
    ISupplierRepository supplierRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<AssignSupplierCommand, ProductSupplierResponse>
{
    public async Task<ProductSupplierResponse> Handle(AssignSupplierCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException($"Product with id '{request.ProductId}' was not found.");
        var supplier = await supplierRepository.GetByIdAsync(request.SupplierId, cancellationToken)
            ?? throw new NotFoundException($"Supplier with id '{request.SupplierId}' was not found.");

        if (await productSupplierRepository.ExistsAsync(request.ProductId, request.SupplierId, cancellationToken))
        {
            throw new ConflictException("The supplier is already assigned to this product.");
        }

        var productSupplier = new ProductSupplier
        {
            ProductId = product.Id,
            SupplierId = supplier.Id
        };

        await productSupplierRepository.AddAsync(productSupplier, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ProductSupplierResponse(product.Id, product.Name, supplier.Id, supplier.Name, supplier.Email);
    }
}
