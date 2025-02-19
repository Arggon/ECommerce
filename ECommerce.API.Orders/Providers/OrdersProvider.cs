using AutoMapper;
using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order = ECommerce.API.Orders.Models.Order;

namespace ECommerce.API.Orders.Providers;

public class OrdersProvider : IOrdersProvider
{
    private readonly OrdersDbContext _context;
    private readonly ILogger<OrdersProvider> _logger;
    private readonly IMapper _mapper;

    public OrdersProvider(OrdersDbContext context, ILogger<OrdersProvider> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
        
        SeedData();
    }

    private void SeedData()
    {
        if (_context.Orders.Any()) return;
        _context.Orders.Add(new Db.Order
        {
            Id = 1,
            CustomerId = 1,
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            Items =
            [
                new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                new Db.OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 5, UnitPrice = 20 }
            ]
        });
        _context.SaveChanges();
    }

    public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
    {
        try
        {
            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Items)
                .ToListAsync();
            if (orders == null || !orders.Any()) return (false, null, "Not found");
            var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
            return (true, result, null);

        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}