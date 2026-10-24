using InventoryManagement.Application.Products.Commands.CreateProduct;
using InventoryManagement.Application.Products.Commands.DeleteProduct;
using InventoryManagement.Application.Products.Commands.UpdateProduct;
using InventoryManagement.Application.Common.Models;
using InventoryManagement.Application.Products.Dtos;
using InventoryManagement.Application.Products.Queries.GetProductById;
using InventoryManagement.Application.Products.Queries.GetProducts;
using InventoryManagement.Application.ProductSuppliers.Commands.AssignSupplier;
using InventoryManagement.Application.ProductSuppliers.Commands.RemoveSupplier;
using InventoryManagement.Application.ProductSuppliers.Dtos;
using InventoryManagement.Application.ProductSuppliers.Queries.GetProductSuppliers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<ProductResponse>>> GetAll([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
    {
        var query = new GetProductsQuery(
            request.PageNumber,
            request.PageSize,
            request.Search,
            request.CategoryId,
            request.IsActive,
            request.MinPrice,
            request.MaxPrice,
            request.SortBy,
            request.SortDirection);
        var response = await sender.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetProductByIdQuery(id), cancellationToken);

        return Ok(response);
    }

    [HttpPost("{productId:guid}/suppliers/{supplierId:guid}")]
    [ProducesResponseType(typeof(ProductSupplierResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductSupplierResponse>> AssignSupplier(Guid productId, Guid supplierId, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new AssignSupplierCommand(productId, supplierId), cancellationToken);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet("{productId:guid}/suppliers")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProductSupplierResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<ProductSupplierResponse>>> GetSuppliers(Guid productId, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetProductSuppliersQuery(productId), cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{productId:guid}/suppliers/{supplierId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveSupplier(Guid productId, Guid supplierId, CancellationToken cancellationToken)
    {
        await sender.Send(new RemoveSupplierCommand(productId, supplierId), cancellationToken);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductResponse>> Update(Guid id, UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(
            id,
            request.Name,
            request.Sku,
            request.Description,
            request.Price,
            request.ReorderLevel,
            request.IsActive,
            request.CategoryId);
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteProductCommand(id), cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Sku,
            request.Description,
            request.Price,
            request.ReorderLevel,
            request.CategoryId);
        var response = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }
}
