using FluentValidation;

namespace InventoryManagement.Application.Stocks.Commands.AdjustStock;

public class AdjustStockCommandValidator : AbstractValidator<AdjustStockCommand>
{
    public AdjustStockCommandValidator()
    {
        RuleFor(command => command.StockId).NotEmpty();
        RuleFor(command => command.NewQuantity).GreaterThanOrEqualTo(0);
        RuleFor(command => command.Reason).NotEmpty().MaximumLength(500);
    }
}
