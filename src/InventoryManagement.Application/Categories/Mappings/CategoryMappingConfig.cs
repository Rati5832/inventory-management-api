using InventoryManagement.Application.Categories.Commands.CreateCategory;
using InventoryManagement.Application.Categories.Dtos;
using InventoryManagement.Domain.Entities;
using Mapster;

namespace InventoryManagement.Application.Categories.Mappings;

public class CategoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCategoryRequest, CreateCategoryCommand>();
        config.NewConfig<CreateCategoryCommand, Category>();
        config.NewConfig<Category, CategoryResponse>();
    }
}
