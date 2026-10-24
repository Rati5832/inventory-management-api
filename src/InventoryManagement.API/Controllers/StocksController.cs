using InventoryManagement.Application.Stocks.Commands.AdjustStock;
using InventoryManagement.Application.Stocks.Commands.StockIn;
using InventoryManagement.Application.Stocks.Commands.StockOut;
using InventoryManagement.Application.Stocks.Commands.TransferStock;
using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Queries.GetAllStocks;
using InventoryManagement.Application.Stocks.Queries.GetLowStocks;
using InventoryManagement.Application.Stocks.Queries.GetStockById;
using InventoryManagement.Application.Stocks.Queries.GetStockMovements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/stocks")]
public class StocksController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<StockResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<StockResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllStocksQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("low-stock")]
    [ProducesResponseType(typeof(IReadOnlyCollection<StockResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<StockResponse>>> GetLowStocks(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetLowStocksQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(StockResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StockResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetStockByIdQuery(id), cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}/movements")]
    [ProducesResponseType(typeof(IReadOnlyCollection<StockMovementHistoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<StockMovementHistoryResponse>>> GetMovements(Guid id, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetStockMovementsQuery(id), cancellationToken);

        return Ok(response);
    }

    [HttpPatch("{id:guid}/adjust")]
    [ProducesResponseType(typeof(StockAdjustmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StockAdjustmentResponse>> Adjust(Guid id, AdjustStockRequest request, CancellationToken cancellationToken)
    {
        var command = new AdjustStockCommand(id, request.NewQuantity, request.Reason);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("stock-in")]
    [ProducesResponseType(typeof(StockMovementResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StockMovementResponse>> StockIn(StockInRequest request, CancellationToken cancellationToken)
    {
        var command = new StockInCommand(request.ProductId, request.WarehouseId, request.Quantity, request.Note);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("stock-out")]
    [ProducesResponseType(typeof(StockMovementResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StockMovementResponse>> StockOut(StockOutRequest request, CancellationToken cancellationToken)
    {
        var command = new StockOutCommand(request.ProductId, request.WarehouseId, request.Quantity, request.Note);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("transfer")]
    [ProducesResponseType(typeof(StockTransferResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<StockTransferResponse>> Transfer(TransferStockRequest request, CancellationToken cancellationToken)
    {
        var command = new TransferStockCommand(
            request.ProductId,
            request.SourceWarehouseId,
            request.DestinationWarehouseId,
            request.Quantity,
            request.Note);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }
}
