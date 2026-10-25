namespace InventoryManagement.Application.Categories.Dtos;

public record CategoryResponse(Guid Id, string Name, string? Description, DateTime CreatedAtUtc);
