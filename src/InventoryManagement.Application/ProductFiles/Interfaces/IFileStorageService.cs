namespace InventoryManagement.Application.ProductFiles.Interfaces;

public interface IFileStorageService
{
    Task<StoredFile> SaveAsync(Stream content, string extension, CancellationToken cancellationToken = default);

    Task<Stream?> OpenReadAsync(string storagePath, CancellationToken cancellationToken = default);

    Task DeleteAsync(string storagePath, CancellationToken cancellationToken = default);
}

public record StoredFile(string StoredName, string StoragePath);
