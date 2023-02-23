
using ECommerce.Domain.Entities;
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.DTOs;

namespace Ecommerce.Sevice.Interfaces;

public interface IOrderService
{
    Task<Response<Order>> CreateAsync(OrderCreationDto order);
    Task<Response<Order>> UpdateAsync(Predicate<Order> predicate, OrderCreationDto order);
    Task<Response<bool>> DeleteAsync(Predicate<Order> predicate);
    Task<Response<Order>> GetByIdAsync(Predicate<Order> predicate);
    Task<Response<List<Order>>> GetAllAsync(Predicate<Order> predicate = null);
}
