using System.Text.Json;
using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task<(bool IsSuccess, IEnumerable<Order>? orders, string? ErrorMessage)> GetOrdersAsync(int customerId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("OrdersService");
            var response = await client.GetAsync($"api/orders/{customerId}");
            
            if (!response.IsSuccessStatusCode) return (false, null, response.ReasonPhrase);
            
            var content = await response.Content.ReadAsByteArrayAsync();
            var option = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            
            var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, option);
            return (true, result, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}