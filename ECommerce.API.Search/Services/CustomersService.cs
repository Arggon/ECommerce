using System.Text.Json;
using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class CustomersService : ICustomersService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CustomersService> _logger;

    public CustomersService(ILogger<CustomersService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(bool IsSuccess, dynamic? Customer, string? ErrorMessage)> GetCustomerAsync(int customerId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("CustomersService");
            var response = await client.GetAsync($"api/customers/{customerId}");
            
            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);
            
            var content = await response.Content.ReadAsByteArrayAsync();
            var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<dynamic>(content, option);
            
            return (true, result, null);
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
        }
    }
}