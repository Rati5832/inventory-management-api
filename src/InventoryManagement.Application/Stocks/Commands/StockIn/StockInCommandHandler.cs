using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Products.Interfaces;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Application.Warehouses.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.StockIn;

public class StockInCommandHandler(
    IStockRepository stockRepository,
    IStockMovementRepository stockMovementRepository,
    IProductRepository productRepository,
    IWarehouseRepository warehouseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<StockInCommand, StockMovementResponse>
{
    public async Task<StockMovementResponse> Handle(StockInCommand request, CancellationToken cancellationToken)
    {
        if (await productRepository.GetByIdAsync(request.ProductId, cancellationToken) is null)
        {
            throw new NotFoundException($"Product with id '{request.ProductId}' was not found.");
        }

        if (await warehouseRepository.GetByIdAsync(request.WarehouseId, cancellationToken) is null)
        {
            throw new NotFoundException($"Warehouse with id '{request.WarehouseId}' was not found.");
        }

        var stock = await stockRepository.GetByProductAndWarehouseAsync(request.ProductId, request.WarehouseId, cancellationToken);

        if (stock is null)
        {
            stock = new Stock
            {
                ProductId = request.ProductId,
                WarehouseId = request.WarehouseId
            };

            await stockRepository.AddAsync(stock, cancellationToken);
        }
        else
        {
            stock.UpdatedAtUtc = DateTime.UtcNow;
        }

        stock.Quantity += request.Quantity;

        var movement = new StockMovement
        {
            Type = StockMovementType.StockIn,
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
