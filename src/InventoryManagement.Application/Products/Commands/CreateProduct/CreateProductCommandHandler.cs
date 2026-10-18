using InventoryManagement.Application.Categories.Interfaces;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Dtos;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Domain.Entities;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken) is null)
        {
            throw new NotFoundException($"Category with id '{request.CategoryId}' was not found.");
        }

        var normalizedSku = request.Sku.Trim().ToUpperInvariant();

        if (await productRepository.ExistsBySkuAsync(normalizedSku, cancellationToken: cancellationToken))
        {
            throw new ConflictException($"Product with SKU '{normalizedSku}' already exists.");
        }

        var product = mapper.Map<Product>(request);
        product.Name = request.Name.Trim();
        product.Sku = normalizedSku;
        product.Description = request.Description?.Trim();

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductResponse>(product);
    }
}
