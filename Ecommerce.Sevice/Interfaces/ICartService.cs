
using ECommerce.Domain.Entities;
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.DTOs;

namespace Ecommerce.Sevice.Interfaces;

public interface ICartService
{
    Task<Response<Cart>> CreateAsync(long userId);
    Task<Response<Cart>> UpdateAsync(long userId, Cart cart);
    Task<Response<bool>> ClearAsync(long userId);
    Task<Response<Cart>> AddProductAsync(long userId, Clothes product);
    Task<Response<Cart>> GetCartAsync(Predicate<Cart> predicate);
    
}
