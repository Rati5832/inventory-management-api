using InventoryManagement.Application.Stocks.Dtos;
using InventoryManagement.Application.Stocks.Queries.GetAllStockMovements;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/stock-movements")]
public class StockMovementsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<StockMovementDetailsResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<StockMovementDetailsResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllStockMovementsQuery(), cancellationToken);

        return Ok(response);
    }
}
