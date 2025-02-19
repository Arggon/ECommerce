using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Db;

public class ProductsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}