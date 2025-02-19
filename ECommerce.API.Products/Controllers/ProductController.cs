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
    
    
}