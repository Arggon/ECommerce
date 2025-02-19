using ECommerce.API.Products.Models;

namespace ECommerce.API.Products.Interfaces;

public interface IProductsProvider
{
    Task<(bool isSuccess, IEnumerable<Product>? products, string? errorMessage)> GetProductsAsync();
    Task<(bool isSuccess, Product? product, string? errorMessage)> GetProductAsync(int id);
}