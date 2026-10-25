using InventoryManagement.Application.ProductFiles.Dtos;
using MediatR;

namespace InventoryManagement.Application.ProductFiles.Queries.DownloadProductFile;

public record DownloadProductFileQuery(Guid ProductId, Guid FileId) : IRequest<ProductFileDownload>;
