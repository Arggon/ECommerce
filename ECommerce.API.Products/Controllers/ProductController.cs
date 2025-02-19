using ECommerce.API.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Products.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductsProvider productsProvider) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync()
    {
        var result = await productsProvider.GetProductsAsync();
        return result.isSuccess ? Ok(result.products) : NotFound(result.errorMessage);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var result = await productsProvider.GetProductAsync(id);
        return result.isSuccess ? Ok(result.product) : NotFound(result.errorMessage);
    }
}