using FluentValidation;

namespace InventoryManagement.Application.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(150);
        RuleFor(command => command.Location).NotEmpty().MaximumLength(250);
    }
}
