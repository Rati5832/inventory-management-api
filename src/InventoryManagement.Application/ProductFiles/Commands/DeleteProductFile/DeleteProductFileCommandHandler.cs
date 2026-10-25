using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.ProductFiles.Interfaces;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Commands.DeleteProductFile;

public class DeleteProductFileCommandHandler(IProductFileRepository productFileRepository, IFileStorageService fileStorageService, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductFileCommand>
{
    public async Task Handle(DeleteProductFileCommand request, CancellationToken cancellationToken)
    {
        var productFile = await productFileRepository.GetByProductAndIdAsync(request.ProductId, request.FileId, cancellationToken)
            ?? throw new NotFoundException($"File with id '{request.FileId}' was not found for product '{request.ProductId}'.");

        await fileStorageService.DeleteAsync(productFile.StoragePath, cancellationToken);
        productFileRepository.Remove(productFile);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
