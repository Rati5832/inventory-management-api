using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.ProductSuppliers.Dtos;
using InventoryManagement.Application.ProductSuppliers.Interfaces;
using MediatR;

namespace InventoryManagement.Application.ProductSuppliers.Queries.GetProductSuppliers;

public class GetProductSuppliersQueryHandler(IProductSupplierRepository productSupplierRepository, IProductRepository productRepository) : IRequestHandler<GetProductSuppliersQuery, IReadOnlyCollection<ProductSupplierResponse>>
{
    public async Task<IReadOnlyCollection<ProductSupplierResponse>> Handle(GetProductSuppliersQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException($"Product with id '{request.ProductId}' was not found.");
        var productSuppliers = await productSupplierRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

        return productSuppliers.Select(productSupplier => new ProductSupplierResponse(
            product.Id,
            product.Name,
            productSupplier.SupplierId,
            productSupplier.Supplier.Name,
            productSupplier.Supplier.Email)).ToList();
    }
}
