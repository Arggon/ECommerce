using AutoMapper;
using ECommerce.API.Products.Db;
using ECommerce.API.Products.Profile;
using ECommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Product.Test;

public class ProductsServiceTest
{
    [Fact]
    public async Task GetProductsReturnsAllProducts()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
            .Options;
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productsProvider = new ProductsProvider(dbContext, null, mapper);

        var product = await productsProvider.GetProductsAsync();
        
        Assert.True(product.isSuccess);
        Assert.True(product.products.Any());
        Assert.Null(product.errorMessage);
    }

    [Fact]
    public async Task GetProductReturnsProductUsingValidId()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
            .Options;
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productsProvider = new ProductsProvider(dbContext, null, mapper);

        var product = await productsProvider.GetProductAsync(1);
        
        Assert.True(product.isSuccess);
        Assert.NotNull(product.product);
        Assert.True(product.product.Id == 1);
        Assert.Null(product.errorMessage);
    }

    [Fact]
    public async Task GetProductReturnsProductUsingInValidId()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInValidId))
            .Options;
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productsProvider = new ProductsProvider(dbContext, null, mapper);

        var product = await productsProvider.GetProductAsync(-1);
        
        Assert.False(product.isSuccess);
        Assert.Null(product.product);
        Assert.NotNull(product.errorMessage);
    }
    
    private void CreateProducts(ProductsDbContext dbContext)
    {
        for (int i = 1; i <= 10; i++)
        {
            dbContext.Products.Add(new Products.Db.Product
            {
                Id = i,
                Name = Guid.NewGuid().ToString(),
                Inventory = i + 10,
                Price = (decimal)(i * 3.14)
            });
        }

        dbContext.SaveChanges();
    }
}