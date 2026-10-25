using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.ProductFiles.Dtos;
using InventoryManagement.Application.ProductFiles.Interfaces;
using InventoryManagement.Domain.Entities;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Commands.UploadProductFile;

public class UploadProductFileCommandHandler(
    IProductFileRepository productFileRepository,
    IProductRepository productRepository,
    IFileStorageService fileStorageService,
    IUnitOfWork unitOfWork) : IRequestHandler<UploadProductFileCommand, ProductFileResponse>
{
    public async Task<ProductFileResponse> Handle(UploadProductFileCommand request, CancellationToken cancellationToken)
    {
        if (await productRepository.GetByIdAsync(request.ProductId, cancellationToken) is null)
        {
            throw new NotFoundException($"Product with id '{request.ProductId}' was not found.");
        }

        var extension = Path.GetExtension(request.OriginalName);
        var storedFile = await fileStorageService.SaveAsync(request.Content, extension, cancellationToken);

        try
        {
            var productFile = new ProductFile
            {
                ProductId = request.ProductId,
                OriginalName = request.OriginalName,
                StoredName = storedFile.StoredName,
                ContentType = request.ContentType,
                SizeInBytes = request.SizeInBytes,
                StoragePath = storedFile.StoragePath
            };

            await productFileRepository.AddAsync(productFile, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ProductFileResponse(
                productFile.Id,
                productFile.ProductId,
                productFile.OriginalName,
                productFile.ContentType,
                productFile.SizeInBytes,
                productFile.CreatedAtUtc);
        }
        catch
        {
            await fileStorageService.DeleteAsync(storedFile.StoragePath, CancellationToken.None);
            throw;
        }
    }
}
