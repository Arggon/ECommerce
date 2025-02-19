using ECommerce.API.Customers.Models;

namespace ECommerce.API.Customers.Interfaces;

public interface ICustomerProvider
{
    Task<(bool isSuccess, IEnumerable<Customer>? customers, string? errorMessage)> GetCustomersAsync();
    Task<(bool isSuccess, Customer? customer, string? errorMessage)> GetCustomerAsync(int id);
}