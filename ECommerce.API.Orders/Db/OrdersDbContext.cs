using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.Db;

public class OrdersDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}