using InventoryManagement.Application.ProductFiles.Dtos;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Commands.UploadProductFile;

public record UploadProductFileCommand(
    Guid ProductId,
    Stream Content,
    string OriginalName,
    string ContentType,
    long SizeInBytes) : IRequest<ProductFileResponse>;
