using FluentAssertions;
using InventoryManagement.Application.ProductFiles.Commands.UploadProductFile;

namespace InventoryManagement.UnitTests.ProductFiles;

public class UploadProductFileCommandValidatorTests
{
    [Fact]
    public void Validate_WhenFileTypeIsNotAllowed_ReturnsValidationError()
    {
        var validator = new UploadProductFileCommandValidator();
        using var content = new MemoryStream([1, 2, 3]);
        var command = new UploadProductFileCommand(Guid.NewGuid(), content, "malware.exe", "application/octet-stream", content.Length);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.ErrorMessage.Contains("Only JPEG, PNG, WEBP, and PDF"));
    }

    [Fact]
    public void Validate_WhenFileIsLargerThanFiveMegabytes_ReturnsValidationError()
    {
        var validator = new UploadProductFileCommandValidator();
        using var content = new MemoryStream([1]);
        var command = new UploadProductFileCommand(Guid.NewGuid(), content, "document.pdf", "application/pdf", 5 * 1024 * 1024 + 1);

        var result = validator.Validate(command);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(error => error.PropertyName == nameof(UploadProductFileCommand.SizeInBytes));
    }
}
