using FluentValidation;

namespace InventoryManagement.Application.ProductSuppliers.Commands.RemoveSupplier;

public class RemoveSupplierCommandValidator : AbstractValidator<RemoveSupplierCommand>
{
    public RemoveSupplierCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.SupplierId).NotEmpty();
    }
}
