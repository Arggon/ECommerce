using AutoMapper;
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Customer = ECommerce.API.Customers.Models.Customer;

namespace ECommerce.API.Customers.Providers;

public class CustomerProvider : ICustomerProvider
{
    private readonly CustomerDbContext _dbContext;
    private readonly ILogger<CustomerDbContext> _logger;
    private readonly IMapper _mapper;

    public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerDbContext> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
        
        SeedData();
    }

    private void SeedData()
    {
        if (_dbContext.Customers.Any()) return;
        var customers = new List<Db.Customer>
        {
            new() { Id = 1, Name = "John Doe", Address = "123 Main St", },
            new() { Id = 2, Name = "Jane Doe", Address = "456 Elm St", },
            new() { Id = 3, Name = "Jack Doe", Address = "789 Maple St", }
        };
            
        _dbContext.Customers.AddRange(customers);
        _dbContext.SaveChanges();
    }

    public async Task<(bool isSuccess, IEnumerable<Customer>? customers, string? errorMessage)> GetCustomersAsync()
    {
        try
        {
            var customers = await _dbContext.Customers.ToListAsync();
            if (customers == null || customers.Count == 0) return (false, null, "Not found");
            var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Customer>>(customers);
            return (true, result, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool isSuccess, Customer? customer, string? errorMessage)> GetCustomerAsync(int id)
    {
        try
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
            if (customer == null) return (false, null, "Not found");
            var result = _mapper.Map<Db.Customer, Customer>(customer);
            return (true, result, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return (false, null, ex.Message);
        }
    }
}