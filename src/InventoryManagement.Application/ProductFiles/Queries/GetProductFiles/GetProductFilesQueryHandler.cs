using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.ProductFiles.Dtos;
using InventoryManagement.Application.ProductFiles.Interfaces;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Queries.GetProductFiles;

public class GetProductFilesQueryHandler(IProductFileRepository productFileRepository, IProductRepository productRepository) : IRequestHandler<GetProductFilesQuery, IReadOnlyCollection<ProductFileResponse>>
{
    public async Task<IReadOnlyCollection<ProductFileResponse>> Handle(GetProductFilesQuery request, CancellationToken cancellationToken)
    {
        if (await productRepository.GetByIdAsync(request.ProductId, cancellationToken) is null)
        {
            throw new NotFoundException($"Product with id '{request.ProductId}' was not found.");
        }

        var files = await productFileRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

        return files.Select(file => new ProductFileResponse(
            file.Id,
            file.ProductId,
            file.OriginalName,
            file.ContentType,
            file.SizeInBytes,
            file.CreatedAtUtc)).ToList();
    }
}
