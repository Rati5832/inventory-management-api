using FluentValidation;

namespace InventoryManagement.Application.ProductFiles.Commands.DeleteProductFile;

public class DeleteProductFileCommandValidator : AbstractValidator<DeleteProductFileCommand>
{
    public DeleteProductFileCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.FileId).NotEmpty();
    }
}
