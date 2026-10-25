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
using InventoryManagement.Application.ProductFiles.Commands.UploadProductFile;
using InventoryManagement.Application.ProductFiles.Commands.DeleteProductFile;
using InventoryManagement.Application.ProductFiles.Dtos;
using InventoryManagement.Application.ProductFiles.Queries.GetProductFiles;
using InventoryManagement.Application.ProductFiles.Queries.DownloadProductFile;
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

    [HttpPost("{productId:guid}/files")]
    [ProducesResponseType(typeof(ProductFileResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductFileResponse>> UploadFile(Guid productId, IFormFile file, CancellationToken cancellationToken)
    {
        await using var content = file.OpenReadStream();
        var command = new UploadProductFileCommand(
            productId,
            content,
            Path.GetFileName(file.FileName),
            file.ContentType,
            file.Length);
        var response = await sender.Send(command, cancellationToken);

        return StatusCode(StatusCodes.Status201Created, response);
    }

    [HttpGet("{productId:guid}/files")]
    [ProducesResponseType(typeof(IReadOnlyCollection<ProductFileResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyCollection<ProductFileResponse>>> GetFiles(Guid productId, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new GetProductFilesQuery(productId), cancellationToken);

        return Ok(response);
    }

    [HttpGet("{productId:guid}/files/{fileId:guid}/download")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DownloadFile(Guid productId, Guid fileId, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new DownloadProductFileQuery(productId, fileId), cancellationToken);

        return File(response.Content, response.ContentType, response.FileName);
    }

    [HttpDelete("{productId:guid}/files/{fileId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFile(Guid productId, Guid fileId, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteProductFileCommand(productId, fileId), cancellationToken);

        return NoContent();
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
