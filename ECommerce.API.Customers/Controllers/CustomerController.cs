using ECommerce.API.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Customers.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerProvider _customerProvider;

    public CustomerController(ICustomerProvider customerProvider)
    {
        _customerProvider = customerProvider;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var result = await _customerProvider.GetCustomersAsync();
        if (result.isSuccess) return Ok(result.customers);
        return NotFound(result.errorMessage);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var result = await _customerProvider.GetCustomerAsync(id);
        if (result.isSuccess) return Ok(result.customer);
        return NotFound(result.errorMessage);
    }
}