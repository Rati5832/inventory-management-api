using InventoryManagement.Application.Common.Exceptions;
using InventoryManagement.Application.Common.Interfaces;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Interfaces;
using InventoryManagement.Application.Warehouses.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Stocks.Commands.TransferStock;

public class TransferStockCommandHandler(
    IStockRepository stockRepository,
    IStockMovementRepository stockMovementRepository,
    IWarehouseRepository warehouseRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<TransferStockCommand, StockTransferResponse>
{
    public async Task<StockTransferResponse> Handle(TransferStockCommand request, CancellationToken cancellationToken)
    {
        var sourceStock = await stockRepository.GetByProductAndWarehouseAsync(request.ProductId, request.SourceWarehouseId, cancellationToken)
            ?? throw new NotFoundException("Stock was not found for the specified product and source warehouse.");

        if (await warehouseRepository.GetByIdAsync(request.DestinationWarehouseId, cancellationToken) is null)
        {
            throw new NotFoundException($"Warehouse with id '{request.DestinationWarehouseId}' was not found.");
        }

        if (sourceStock.Quantity < request.Quantity)
        {
            throw new ConflictException($"Insufficient stock. Available quantity is {sourceStock.Quantity}.");
        }

        var destinationStock = await stockRepository.GetByProductAndWarehouseAsync(request.ProductId, request.DestinationWarehouseId, cancellationToken);

        if (destinationStock is null)
        {
            destinationStock = new Stock
            {
                ProductId = request.ProductId,
                WarehouseId = request.DestinationWarehouseId
            };

            await stockRepository.AddAsync(destinationStock, cancellationToken);
        }
        else
        {
            destinationStock.UpdatedAtUtc = DateTime.UtcNow;
        }

        sourceStock.Quantity -= request.Quantity;
        sourceStock.UpdatedAtUtc = DateTime.UtcNow;
        destinationStock.Quantity += request.Quantity;

        var note = request.Note?.Trim();

        var sourceMovement = new StockMovement
        {
            Type = StockMovementType.StockOut,
            Quantity = request.Quantity,
            Note = note,
            Stock = sourceStock
        };

        var destinationMovement = new StockMovement
        {
            Type = StockMovementType.StockIn,
            Quantity = request.Quantity,
            Note = note,
            Stock = destinationStock
        };

        await stockMovementRepository.AddAsync(sourceMovement, cancellationToken);
        await stockMovementRepository.AddAsync(destinationMovement, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new StockTransferResponse(
            sourceMovement.Id,
            destinationMovement.Id,
            sourceStock.Id,
            destinationStock.Id,
            request.ProductId,
            request.SourceWarehouseId,
            request.DestinationWarehouseId,
            request.Quantity,
            sourceStock.Quantity,
            destinationStock.Quantity,
            note);
    }
}
