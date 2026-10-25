namespace InventoryManagement.Application.ProductFiles.Dtos;

public record ProductFileResponse(
    Guid Id,
    Guid ProductId,
    string OriginalName,
    string ContentType,
    long SizeInBytes,
    DateTime CreatedAtUtc);
