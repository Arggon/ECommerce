using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces;

public interface IProductService
{
    Task<(bool IsSuccess, IEnumerable<Product>? Products, string? ErrorMessage)> GetProductsAsync();
}