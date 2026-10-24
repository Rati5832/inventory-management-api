using InventoryManagement.Application.Products.Commands.CreateProduct;
using InventoryManagement.Application.Products.Dtos;
using InventoryManagement.Application.Products.Models;
using InventoryManagement.Application.Products.Queries.GetProducts;
using InventoryManagement.Domain.Entities;
using Mapster;

namespace InventoryManagement.Application.Products.Mappings;

public class ProductMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateProductCommand, Product>();
        config.NewConfig<Product, ProductResponse>();
        config.NewConfig<GetProductsQuery, ProductFilter>();
    }
}
