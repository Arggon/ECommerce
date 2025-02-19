using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Customers.Db;

public class CustomerDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }
}