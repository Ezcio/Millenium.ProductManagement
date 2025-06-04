using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Core.Commands;
using ProductManagement.Core.DTO;
using ProductManagement.Core.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger,
        IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [SwaggerOperation("Get Products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var products = await _mediator.Send(new GetProducts());

        if (products.Any())
        {
            _logger.LogInformation("Retrieved {Count} products.", products.Count());
            return Ok(products);
        }

        _logger.LogWarning("No products found.");
        return NotFound();
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get Product by id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var product = await _mediator.Send(new GetProductById(id));

        if (product is null)
        {
            _logger.LogWarning("Product with ID {ProductId} not found.", id);
            return NotFound();
        }

        _logger.LogInformation("Product with ID {ProductId} retrieved successfully.", id);
        return Ok(product);
    }

    [HttpPost]
    [SwaggerOperation("Add product")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] AddProduct product)
    {
        var id = await _mediator.Send(product);

        _logger.LogInformation("Product created successfully with ID {ProductId}.", id);
        return CreatedAtAction(nameof(Get), new { id = id }, null);
    }

    [HttpPut]
    [SwaggerOperation("Update product")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromBody] UpdateProduct updateProduct)
    {
        var response = await _mediator.Send(updateProduct);

        if (response)
        {
            _logger.LogInformation("Product with ID {ProductId} updated successfully.", updateProduct.Id);
            return CreatedAtAction(nameof(Get), new { id = updateProduct.Id }, null);
        }

        _logger.LogWarning("Product with ID {ProductId} not found for update.", updateProduct.Id);
        return NotFound();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Remove product")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _mediator.Send(new DeleteProduct(id));

        if (success)
        {
            _logger.LogInformation("Product with ID {ProductId} deleted successfully.", id);
            return NoContent();
        }

        _logger.LogWarning("Product with ID {ProductId} not found for deletion.", id);
        return NotFound();
    }
}
