using MediatR;

namespace InventoryManagement.Application.ProductFiles.Commands.DeleteProductFile;

public record DeleteProductFileCommand(Guid ProductId, Guid FileId) : IRequest;
