using InventoryManagement.Application.Warehouses.Commands.CreateWarehouse;
using InventoryManagement.Application.Warehouses.Dtos;
using InventoryManagement.Domain.Entities;
using Mapster;

namespace InventoryManagement.Application.Warehouses.Mappings;

public class WarehouseMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateWarehouseCommand, Warehouse>();
        config.NewConfig<Warehouse, WarehouseResponse>();
    }
}
