
using Ecommerce.Sevice.Helpers;
using ECommerce.Domain.Entities;

namespace Ecommerce.Sevice.Interfaces
{
    public interface IUserService
    {
        Task<Response<User>> CreateAsync(UserCreationDto user);
        Task<Response<User>> UpdateAsync(Predicate<User> predicate, UserCreationDto user);
        Task<Response<bool>> DeleteAsync(Predicate<User> predicate);
        Task<Response<User>> GetByIdAsync(Predicate<User> predicate);
        Task<Response<List<User>>> GetAllAsync(Predicate<User> predicate = null);
        Task<Response<User>> ChechkForExists(string login,string password);
    }
}
