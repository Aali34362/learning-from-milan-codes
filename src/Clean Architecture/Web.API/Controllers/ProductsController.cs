using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.GetById;
using Application.Products.Update;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductRequest request)
    {
        var command = new CreateProductCommand(
            request.Name,
            request.Sku,
            request.Currency,
        request.Amount);

        await _sender.Send(command);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts(
        string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize)
    {
        var query = new GetProductsQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

        var products = await _sender.Send(query);

        return Ok(products);
    }

    [HttpGet("{id}", Name = nameof(GetProduct))]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        try
        {
            var productResponse = await _sender.Send(new GetProductQuery(new ProductId(id)));

            return Ok(productResponse);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}", Name = nameof(UpdateProduct))]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductRequest request)
    {
        try
        {
            var command = new UpdateProductCommand(
                new ProductId(id),
                request.Name,
                request.Sku,
                request.Currency,
                request.Amount);

            await _sender.Send(command);

            return NoContent();
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id}", Name = nameof(DeleteProduct))]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        try
        {
            await _sender.Send(new DeleteProductCommand(new ProductId(id)));

            return NoContent();
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}
