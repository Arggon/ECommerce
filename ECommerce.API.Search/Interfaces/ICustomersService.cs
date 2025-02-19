using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces;

public interface ICustomersService
{
    Task<(bool IsSuccess, dynamic? Customer, string? ErrorMessage)> GetCustomerAsync(int customerId);
}