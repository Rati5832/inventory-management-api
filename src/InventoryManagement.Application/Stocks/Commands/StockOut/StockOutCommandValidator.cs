using FluentValidation;

namespace InventoryManagement.Application.Stocks.Commands.StockOut;

public class StockOutCommandValidator : AbstractValidator<StockOutCommand>
{
    public StockOutCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.WarehouseId).NotEmpty();
        RuleFor(command => command.Quantity).GreaterThan(0);
        RuleFor(command => command.Note).MaximumLength(500);
    }
}
