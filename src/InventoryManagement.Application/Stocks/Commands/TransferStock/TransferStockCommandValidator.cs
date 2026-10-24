using FluentValidation;

namespace InventoryManagement.Application.Stocks.Commands.TransferStock;

public class TransferStockCommandValidator : AbstractValidator<TransferStockCommand>
{
    public TransferStockCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.SourceWarehouseId).NotEmpty();
        RuleFor(command => command.DestinationWarehouseId).NotEmpty().NotEqual(command => command.SourceWarehouseId);
        RuleFor(command => command.Quantity).GreaterThan(0);
        RuleFor(command => command.Note).MaximumLength(500);
    }
}
