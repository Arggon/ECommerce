using ECommerce.API.Search.Interfaces;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Services;

public class SearchService : ISearchService
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICustomersService _customersService;

    public SearchService(IOrderService orderService, IProductService productService, ICustomersService customersService)
    {
        _orderService = orderService;
        _productService = productService;
        _customersService = customersService;
    }
    public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(SearchTerm term)
    {
        var orderResult = await _orderService.GetOrdersAsync(term.CustomerId);
        var productResult = await _productService.GetProductsAsync();
        var customerResult = await _customersService.GetCustomerAsync(term.CustomerId);
        
        if (!orderResult.IsSuccess) return (false, null);

        foreach (var order in orderResult.orders)
        {
            foreach (var item in order.Items)
            {
                item.ProductName = productResult.IsSuccess
                    ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                    : "Product information is not available";
            }
        }
        
        var result = new
        {
            Customer = customerResult.IsSuccess ? customerResult.Customer : new { Name = "Customer information is not available" },
            Orders = orderResult.orders
        };
        
        return (true, result);
    }
}