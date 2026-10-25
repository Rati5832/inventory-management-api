using FluentValidation;

namespace InventoryManagement.Application.ProductFiles.Commands.UploadProductFile;

public class UploadProductFileCommandValidator : AbstractValidator<UploadProductFileCommand>
{
    private const long MaxFileSize = 5 * 1024 * 1024;
    private static readonly IReadOnlyDictionary<string, string> AllowedFileTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        [".jpg"] = "image/jpeg",
        [".jpeg"] = "image/jpeg",
        [".png"] = "image/png",
        [".webp"] = "image/webp",
        [".pdf"] = "application/pdf"
    };

    public UploadProductFileCommandValidator()
    {
        RuleFor(command => command.ProductId).NotEmpty();
        RuleFor(command => command.OriginalName).NotEmpty().MaximumLength(255);
        RuleFor(command => command.ContentType).NotEmpty().MaximumLength(100);
        RuleFor(command => command.SizeInBytes).GreaterThan(0).LessThanOrEqualTo(MaxFileSize);
        RuleFor(command => command).Must(HaveAllowedFileType).WithMessage("Only JPEG, PNG, WEBP, and PDF files are allowed.");
    }

    private static bool HaveAllowedFileType(UploadProductFileCommand command)
    {
        var extension = Path.GetExtension(command.OriginalName);
        return AllowedFileTypes.TryGetValue(extension, out var contentType) &&
            string.Equals(contentType, command.ContentType, StringComparison.OrdinalIgnoreCase);
    }
}
