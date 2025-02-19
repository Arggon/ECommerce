using System.Text.Json;
using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMessage)> GetProductsAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ProductsService");
            var response = await client.GetAsync("api/products");
            
            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);
            
            var content = await response.Content.ReadAsByteArrayAsync();
            var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, option);
            return (true, result, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}