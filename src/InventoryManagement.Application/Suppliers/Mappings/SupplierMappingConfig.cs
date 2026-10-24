using InventoryManagement.Application.Suppliers.Commands.CreateSupplier;
using InventoryManagement.Application.Suppliers.Dtos;
using InventoryManagement.Domain.Entities;
using Mapster;

namespace InventoryManagement.Application.Suppliers.Mappings;

public class SupplierMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateSupplierCommand, Supplier>();
        config.NewConfig<Supplier, SupplierResponse>();
    }
}
