using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.AdjustStock;

public class AdjustStockCommandHandler(IStockRepository stockRepository, IStockMovementRepository stockMovementRepository, IUnitOfWork unitOfWork) : IRequestHandler<AdjustStockCommand, StockAdjustmentResponse>
{
    public async Task<StockAdjustmentResponse> Handle(AdjustStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await stockRepository.GetByIdAsync(request.StockId, cancellationToken)
            ?? throw new NotFoundException($"Stock with id '{request.StockId}' was not found.");

        if (stock.Quantity == request.NewQuantity)
        {
            throw new ConflictException($"Stock quantity is already {request.NewQuantity}.");
        }

        var previousQuantity = stock.Quantity;
        var difference = request.NewQuantity - previousQuantity;
        stock.Quantity = request.NewQuantity;
        stock.UpdatedAtUtc = DateTime.UtcNow;

        var movement = new StockMovement
        {
            Type = StockMovementType.Adjustment,
            Quantity = Math.Abs(difference),
            PreviousQuantity = previousQuantity,
            NewQuantity = request.NewQuantity,
            Note = request.Reason.Trim(),
            Stock = stock
        };

        await stockMovementRepository.AddAsync(movement, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StockAdjustmentResponse(
            movement.Id,
            stock.Id,
            previousQuantity,
            stock.Quantity,
            difference,
            movement.Note,
            movement.CreatedAtUtc);
    }
}
