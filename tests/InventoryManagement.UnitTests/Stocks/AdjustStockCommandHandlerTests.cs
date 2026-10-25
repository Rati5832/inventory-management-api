using FluentAssertions;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Stocks.Commands.AdjustStock;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using Moq;

namespace InventoryManagement.UnitTests.Stocks;

public class AdjustStockCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenQuantityChanges_StoresPreviousAndNewQuantities()
    {
        var stockId = Guid.NewGuid();
        var stock = new Stock { Id = stockId, Quantity = 10 };
        StockMovement? addedMovement = null;
        var stockRepository = new Mock<IStockRepository>();
        var movementRepository = new Mock<IStockMovementRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        stockRepository.Setup(repository => repository.GetByIdAsync(stockId, It.IsAny<CancellationToken>())).ReturnsAsync(stock);
        movementRepository.Setup(repository => repository.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()))
            .Callback<StockMovement, CancellationToken>((movement, _) => addedMovement = movement)
            .Returns(Task.CompletedTask);
        unitOfWork.Setup(work => work.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        var handler = new AdjustStockCommandHandler(stockRepository.Object, movementRepository.Object, unitOfWork.Object);

        var response = await handler.Handle(new AdjustStockCommand(stockId, 6, " Physical count "), CancellationToken.None);

        stock.Quantity.Should().Be(6);
        response.PreviousQuantity.Should().Be(10);
        response.NewQuantity.Should().Be(6);
        response.Difference.Should().Be(-4);
        addedMovement.Should().NotBeNull();
        addedMovement!.Type.Should().Be(StockMovementType.Adjustment);
        addedMovement.Quantity.Should().Be(4);
        addedMovement.PreviousQuantity.Should().Be(10);
        addedMovement.NewQuantity.Should().Be(6);
    }

    [Fact]
    public async Task Handle_WhenQuantityIsUnchanged_ThrowsConflict()
    {
        var stockId = Guid.NewGuid();
        var stockRepository = new Mock<IStockRepository>();
        var movementRepository = new Mock<IStockMovementRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        stockRepository.Setup(repository => repository.GetByIdAsync(stockId, It.IsAny<CancellationToken>())).ReturnsAsync(new Stock { Id = stockId, Quantity = 10 });
        var handler = new AdjustStockCommandHandler(stockRepository.Object, movementRepository.Object, unitOfWork.Object);

        Func<Task> act = () => handler.Handle(new AdjustStockCommand(stockId, 10, "Physical count"), CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*already 10*");
        movementRepository.Verify(repository => repository.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
