using Learn.Blazor.Net6.Pag.Models.Product;
using Learn.Blazor.Net6.Pag.Server.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Blazor.Net6.Pag.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    public ProductsController(IProductService service) =>
        _service = service;

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Index(CancellationToken token)
    {
        var models = await _service.GetAllAsync(token);
        var views = models
            .Select(m => new ProductViewModel(m))
            .ToList();

        return views.Any() ? Ok(views) : NoContent();
    }
}