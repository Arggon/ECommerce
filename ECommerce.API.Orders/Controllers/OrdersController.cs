using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Orders.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController(IOrdersProvider ordersProvider) : ControllerBase
{
    [HttpGet("{customerId:int}")]
    public async Task<ActionResult<Order>> GetOrder(int customerId)
    {
        var result = await ordersProvider.GetOrdersAsync(customerId);
        if (result.IsSuccess) return Ok(result.Orders);
        return NotFound();
    }
}