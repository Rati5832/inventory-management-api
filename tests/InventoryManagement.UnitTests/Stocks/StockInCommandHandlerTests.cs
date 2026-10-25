using FluentAssertions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.Stocks.Commands.StockIn;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Application.Warehouses.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using Moq;

namespace InventoryManagement.UnitTests.Stocks;

public class StockInCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenStockExists_IncreasesQuantityAndCreatesMovement()
    {
        var productId = Guid.NewGuid();
        var warehouseId = Guid.NewGuid();
        var stock = new Stock { Id = Guid.NewGuid(), ProductId = productId, WarehouseId = warehouseId, Quantity = 10 };
        StockMovement? addedMovement = null;
        var stockRepository = new Mock<IStockRepository>();
        var movementRepository = new Mock<IStockMovementRepository>();
        var productRepository = new Mock<IProductRepository>();
        var warehouseRepository = new Mock<IWarehouseRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        productRepository.Setup(repository => repository.GetByIdAsync(productId, It.IsAny<CancellationToken>())).ReturnsAsync(new Product { Id = productId });
        warehouseRepository.Setup(repository => repository.GetByIdAsync(warehouseId, It.IsAny<CancellationToken>())).ReturnsAsync(new Warehouse { Id = warehouseId });
        stockRepository.Setup(repository => repository.GetByProductAndWarehouseAsync(productId, warehouseId, It.IsAny<CancellationToken>())).ReturnsAsync(stock);
        movementRepository.Setup(repository => repository.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
            .Callback<StockMovement, CancellationToken>((movement, _) => addedMovement = movement)
            .Returns(Task.CompletedTask);
        unitOfWork.Setup(work => work.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var handler = new StockInCommandHandler(stockRepository.Object, movementRepository.Object, productRepository.Object, warehouseRepository.Object, unitOfWork.Object);

        var response = await handler.Handle(new StockInCommand(productId, warehouseId, 5, " Delivery "), CancellationToken.None);

        stock.Quantity.Should().Be(15);
        response.CurrentQuantity.Should().Be(15);
        addedMovement.Should().NotBeNull();
        addedMovement!.Type.Should().Be(StockMovementType.StockIn);
        addedMovement.Quantity.Should().Be(5);
        addedMovement.Note.Should().Be("Delivery");
    }
}
