namespace InventoryManagement.Application.ProductFiles.Dtos;

public record ProductFileDownload(Stream Content, string ContentType, string FileName);
