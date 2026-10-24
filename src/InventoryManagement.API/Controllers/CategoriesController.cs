using InventoryManagement.Application.Categories.Commands.DeleteCategory;
using InventoryManagement.Application.Categories.Commands.CreateCategory;
using InventoryManagement.Application.Categories.Commands.UpdateCategory;
using InventoryManagement.Application.Categories.Dtos;
using InventoryManagement.Application.Categories.Queries.GetAllCategories;
using InventoryManagement.Application.Categories.Queries.GetCategoryById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CategoryResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetAllCategoriesQuery(), cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetCategoryByIdQuery(id), cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CategoryResponse>> Update(Guid id, UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(id, request.Name, request.Description);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteCategoryCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CategoryResponse>> Create(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.Name, request.Description);
        var response = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
