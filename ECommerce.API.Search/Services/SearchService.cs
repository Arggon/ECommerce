using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class SearchService : ISearchService
{
    private readonly IOrderService _orderService;

    public SearchService(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(SearchTerm term)
    {
        var orderResult = await _orderService.GetOrdersAsync(term.CustomerId);
        
        if (!orderResult.IsSuccess) return (false, null);
        
        var result = new
        {
            Orders = orderResult.orders
        };
        
        return (true, result);
    }
}