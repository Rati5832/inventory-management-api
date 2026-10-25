using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.ProductFiles.Dtos;
using InventoryManagement.Application.ProductFiles.Interfaces;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Queries.DownloadProductFile;

public class DownloadProductFileQueryHandler(IProductFileRepository productFileRepository, IFileStorageService fileStorageService) : IRequestHandler<DownloadProductFileQuery, ProductFileDownload>
{
    public async Task<ProductFileDownload> Handle(DownloadProductFileQuery request, CancellationToken cancellationToken)
    {
        var productFile = await productFileRepository.GetByProductAndIdAsync(request.ProductId, request.FileId, cancellationToken)
            ?? throw new NotFoundException($"File with id '{request.FileId}' was not found for product '{request.ProductId}'.");
        var content = await fileStorageService.OpenReadAsync(productFile.StoragePath, cancellationToken)
            ?? throw new NotFoundException("The stored file was not found.");

        return new ProductFileDownload(content, productFile.ContentType, productFile.OriginalName);
    }
}
