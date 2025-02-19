using AutoMapper;
using ECommerce.API.Orders.Models;

namespace ECommerce.API.Orders.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Db.Order, Order>();
        CreateMap<Db.OrderItem, OrderItem>();
    }
}