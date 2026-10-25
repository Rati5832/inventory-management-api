using FluentAssertions;
using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Stocks.Commands.StockOut;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Domain.Entities;
using Moq;

namespace InventoryManagement.UnitTests.Stocks;

public class StockOutCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenQuantityIsInsufficient_ThrowsConflictAndDoesNotChangeStock()
    {
        var productId = Guid.NewGuid();
        var warehouseId = Guid.NewGuid();
        var stock = new Stock { ProductId = productId, WarehouseId = warehouseId, Quantity = 3 };
        var stockRepository = new Mock<IStockRepository>();
        var movementRepository = new Mock<IStockMovementRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();
        stockRepository.Setup(repository => repository.GetByProductAndWarehouseAsync(productId, warehouseId, It.IsAny<CancellationToken>())).ReturnsAsync(stock);
        var handler = new StockOutCommandHandler(stockRepository.Object, movementRepository.Object, unitOfWork.Object);

        Func<Task> act = () => handler.Handle(new StockOutCommand(productId, warehouseId, 5, null), CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>().WithMessage("*Available quantity is 3*");
        stock.Quantity.Should().Be(3);
        movementRepository.Verify(repository => repository.AddAsync(It.IsAny<StockMovement>(), It.IsAny<CancellationToken>()), Times.Never);
        unitOfWork.Verify(work => work.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}
