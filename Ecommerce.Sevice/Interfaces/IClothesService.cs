
using Ecommerce.Sevice.DTOs;
using Ecommerce.Sevice.Helpers;
using ECommerce.Domain.Entities;

namespace Ecommerce.Sevice.Interfaces;

public interface IClothesService
{
    Task<Response<Clothes>> CreateAsync(ClothesCreationDto product);
    Task<Response<Clothes>> UpdateAsync(Predicate<Clothes> predicate, ClothesCreationDto product);
    Task<Response<bool>> DeleteAsync(Predicate<Clothes> predicate);
    Task<Response<Clothes>> GetByIdAsync(Predicate<Clothes> predicate);
    Task<Response<List<Clothes>>> GetAllAsync(Predicate<Clothes> predicate = null);

}
