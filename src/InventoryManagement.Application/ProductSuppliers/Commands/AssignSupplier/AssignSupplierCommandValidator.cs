using FluentValidation;

namespace InventoryManagement.Application.ProductSuppliers.Commands.AssignSupplier;

public class AssignSupplierCommandValidator : AbstractValidator<AssignSupplierCommand>
{
    public AssignSupplierCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.SupplierId).NotEmpty();
    }
}
