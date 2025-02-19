using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces;

public interface IOrderService
{
    Task<(bool IsSuccess, IEnumerable<Order>? orders, string? ErrorMessage)> GetOrdersAsync(int id);
}