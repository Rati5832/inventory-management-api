using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.StockOut;

public class StockOutCommandHandler(
    IStockRepository stockRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<StockOutCommand, StockMovementResponse>
{
    public async Task<StockMovementResponse> Handle(StockOutCommand request, CancellationToken cancellationToken)
    {
        var stock = await stockRepository.GetByProductAndWarehouseAsync(request.ProductId, request.WarehouseId, cancellationToken)
            ?? throw new NotFoundException("Stock was not found for the specified product and warehouse.");

        if (stock.Quantity < request.Quantity)
        {
            throw new ConflictException($"Insufficient stock. Available quantity is {stock.Quantity}.");
        }

        stock.Quantity -= request.Quantity;
        stock.UpdatedAtUtc = DateTime.UtcNow;

        var movement = new StockMovement
        {
            Type = StockMovementType.StockOut,
            Quantity = request.Quantity,
            Note = request.Note?.Trim(),
            Stock = stock
        };

        await stockMovementRepository.AddAsync(movement, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StockMovementResponse(
            movement.Id,
            stock.Id,
            stock.ProductId,
            stock.WarehouseId,
            movement.Type,
            movement.Quantity,
            stock.Quantity,
            movement.Note,
            movement.CreatedAtUtc);
    }
}
