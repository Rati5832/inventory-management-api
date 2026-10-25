using InventoryManagement.Application.ProductFiles.Interfaces;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.Infrastructure.Persistence;

public class LocalFileStorageService(IConfiguration configuration) : IFileStorageService
{
    private readonly string rootPath = Path.GetFullPath(configuration["FileStorage:ProductFilesPath"] ?? "uploads/products");

    public async Task<StoredFile> SaveAsync(Stream content, string extension, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(rootPath);

        var storedName = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        var fullPath = Path.Combine(rootPath, storedName);

        await using var output = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true);
        await content.CopyToAsync(output, cancellationToken);

        return new StoredFile(storedName, fullPath);
    }

    public Task<Stream?> OpenReadAsync(string storagePath, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var fullPath = GetValidatedPath(storagePath);

        if (!File.Exists(fullPath))
        {
            return Task.FromResult<Stream?>(null);
        }

        Stream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read, 81920, true);
        return Task.FromResult<Stream?>(stream);
    }

    public Task DeleteAsync(string storagePath, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var fullPath = GetValidatedPath(storagePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return Task.CompletedTask;
    }

    private string GetValidatedPath(string storagePath)
    {
        var fullPath = Path.GetFullPath(storagePath);
        var rootPathWithSeparator = rootPath.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

        if (!fullPath.StartsWith(rootPathWithSeparator, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The file path is outside the configured storage directory.");
        }

        return fullPath;
    }
}
