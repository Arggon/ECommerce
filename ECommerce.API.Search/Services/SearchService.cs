using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class SearchService : ISearchService
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;

    public SearchService(IOrderService orderService, IProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }
    public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(SearchTerm term)
    {
        var orderResult = await _orderService.GetOrdersAsync(term.CustomerId);
        var productResult = await _productService.GetProductsAsync();
        
        if (!orderResult.IsSuccess) return (false, null);

        foreach (var order in orderResult.orders)
        {
            foreach (var item in order.Items)
            {
                item.ProductName = productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name;
            }
        }
        
        var result = new
        {
            Orders = orderResult.orders
        };
        
        return (true, result);
    }
}