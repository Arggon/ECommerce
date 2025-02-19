namespace ECommerce.API.Search.Models;

public class Order
{
    public int Id { get; set; }
    public DateOnly OrderDate { get; set; }
    public decimal Total { get; set; }
    public List<OrderItem> Items { get; set; } = new();
}