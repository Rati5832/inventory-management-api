using FluentAssertions;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.ProductSuppliers.Commands.AssignSupplier;
using InventoryManagement.Application.ProductSuppliers.Interfaces;
using InventoryManagement.Application.Suppliers.Interfaces;
using InventoryManagement.Domain.Entities;
using Moq;

namespace InventoryManagement.UnitTests.ProductSuppliers;

public class AssignSupplierCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenAssignmentAlreadyExists_ThrowsConflict()
    {
        var productId = Guid.NewGuid();
        var supplierId = Guid.NewGuid();
        var productSupplierRepository = new Mock<IProductSupplierRepository>();
        var productRepository = new Mock<IProductRepository>();
        var supplierRepository = new Mock<ISupplierRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        productRepository.Setup(repository => repository.GetByIdAsync(productId, It.IsAny<CancellationToken>())).ReturnsAsync(new Product { Id = productId });
        supplierRepository.Setup(repository => repository.GetByIdAsync(supplierId, It.IsAny<CancellationToken>())).ReturnsAsync(new Supplier { Id = supplierId });
        productSupplierRepository.Setup(repository => repository.ExistsAsync(productId, supplierId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var handler = new AssignSupplierCommandHandler(productSupplierRepository.Object, productRepository.Object, supplierRepository.Object, unitOfWork.Object);

        Func<Task> act = () => handler.Handle(new AssignSupplierCommand(productId, supplierId), CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already assigned*");
        productSupplierRepository.Verify(repository => repository.AddAsync(It.IsAny<ProductSupplier>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
