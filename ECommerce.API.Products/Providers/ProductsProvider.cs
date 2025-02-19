using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product = ECommerce.API.Products.Models.Product;

namespace ECommerce.API.Products.Providers;

public class ProductsProvider : IProductsProvider
{
    private readonly ProductsDbContext _productsDbContext;
    private readonly ILogger<ProductsProvider> _logger;
    private readonly IMapper _mapper;

    public ProductsProvider(ProductsDbContext productsDbContext, ILogger<ProductsProvider> logger, IMapper mapper)
    {
        _productsDbContext = productsDbContext;
        _logger = logger;
        _mapper = mapper;
        _logger.LogInformation("ProductsProvider created");

        SeedData();
    }

    private void SeedData()
    {
        if (_productsDbContext.Products.Any()) return;
        var products = new List<Db.Product>
        {
            new() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 100 },
            new() { Id = 2, Name = "Mouse", Price = 10, Inventory = 50 },
            new() { Id = 3, Name = "Monitor", Price = 100, Inventory = 10 }
        };

        _productsDbContext.Products.AddRange(products);

        _productsDbContext.SaveChanges();
    }

    public async Task<(bool isSuccess, IEnumerable<Product>? products, string? errorMessage)> GetProductsAsync()
    {
        try
        {
            var products = await _productsDbContext.Products.ToListAsync();
            if (products == null || products.Count == 0) return (false, null, "Not found");
            var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Product>>(products);
            return (true, result, null);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool isSuccess, Product? product, string? errorMessage)> GetProductAsync(int id)
    {
        try
        {
            var product = await _productsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return (false, null, "Product not found");
            var result = _mapper.Map<Db.Product, Product>(product);
            return (true, result, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return (false, null, ex.Message);
        }
    }
}