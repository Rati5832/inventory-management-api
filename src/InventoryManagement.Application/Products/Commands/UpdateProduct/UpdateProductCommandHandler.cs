using InventoryManagement.Application.Categories.Interfaces;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Dtos;
using InventoryManagement.Application.Products.Interfaces;
using MapsterMapper;
using MediatR;

namespace InventoryManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateProductCommand, ProductResponse>
{
    public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException($"Product with id '{request.Id}' was not found.");

        if (await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken) is null)
        {
            throw new NotFoundException($"Category with id '{request.CategoryId}' was not found.");
        }

        var normalizedSku = request.Sku.Trim().ToUpperInvariant();

        if (await productRepository.ExistsBySkuAsync(normalizedSku, request.Id, cancellationToken))
        {
            throw new ConflictException($"Product with SKU '{normalizedSku}' already exists.");
        }

        product.Name = request.Name.Trim();
        product.Sku = normalizedSku;
        product.Description = request.Description?.Trim();
        product.Price = request.Price;
        product.ReorderLevel = request.ReorderLevel;
        product.IsActive = request.IsActive;
        product.CategoryId = request.CategoryId;
        product.UpdatedAtUtc = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductResponse>(product);
    }
}
