using FluentValidation;

namespace InventoryManagement.Application.Stocks.Commands.StockIn;

public class StockInCommandValidator : AbstractValidator<StockInCommand>
{
    public StockInCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.WarehouseId).NotEmpty();
        RuleFor(command => command.Quantity).GreaterThan(0);
        RuleFor(command => command.Note).MaximumLength(500);
    }
}
