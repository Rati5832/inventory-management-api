using InventoryManagement.Application.Warehouses.Commands.CreateWarehouse;
using InventoryManagement.Application.Warehouses.Commands.DeleteWarehouse;
using InventoryManagement.Application.Warehouses.Commands.UpdateWarehouse;
using InventoryManagement.Application.Warehouses.Dtos;
using InventoryManagement.Application.Warehouses.Queries.GetAllWarehouses;
using InventoryManagement.Application.Warehouses.Queries.GetWarehouseById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/warehouses")]
public class WarehousesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<WarehouseResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<WarehouseResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllWarehousesQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WarehouseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WarehouseResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetWarehouseByIdQuery(id), cancellationToken);

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WarehouseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WarehouseResponse>> Update(Guid id, UpdateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateWarehouseCommand(id, request.Name, request.Location);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteWarehouseCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(WarehouseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<WarehouseResponse>> Create(CreateWarehouseRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWarehouseCommand(request.Name, request.Location);
        var response = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
