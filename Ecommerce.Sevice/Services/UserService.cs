
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.Interfaces;
using ECommerce.Domain.Entities;
using ECommmerce.Data.Repositories;
using Raaya.Domain.Enums;

namespace Ecommerce.Sevice.Services;

public class UserService : IUserService
{
    private readonly GenericRepository<User> genericRepository = new GenericRepository<User>();
    private readonly ICartService cartService = new CartService();
    private long lastId;

    public async Task<Response<User>> ChechkForExists(string login, string password)
    {
        var users = await this.genericRepository.GetAllAsync(x => x.Id > 0);
        var user = users.FirstOrDefault(x => x.Login == login && x.Password == password);
        if (user == null)
        {
            return new Response<User>()
            {
                StatusCode = 404,
                Message = "not found",
                Value = null
            };
        }
        return new Response<User>()
        {
            StatusCode = 200,
            Message = "Succes",
            Value = user
        };


    }

    public async Task<Response<User>> CreateAsync(UserCreationDto user)
    {
        var models = await this.genericRepository.GetAllAsync(x => x.Id > 0);
        if (models.Count == 0)
            lastId = 1;
        else
            lastId = (models[models.Count - 1].Id) + 1;

        var model = models.FirstOrDefault(x => x.Phone == user.Phone);
        if (model is null)
        {
            var mappedModel = new User()
            {
                Id = lastId,
                CartId = lastId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Login = user.Login,
                Password = user.Password,
                CreatedAt = DateTime.UtcNow
            };
            var result = await this.genericRepository.CreateAsync(mappedModel);
            var a = await cartService.CreateAsync(mappedModel.Id);
            return new Response<User>()
            {
                StatusCode = 200,
                Message = "Success",
                Value = result
            };
        }
        
        return new Response<User>()
        {
            StatusCode = 403,
            Message = "Already exists",
            Value = null
        };
    }

    public async Task<Response<bool>> DeleteAsync(Predicate<User> predicate)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "User is not found",
                Value = false
            };

        await this.genericRepository.DeleteAysnc(predicate);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async Task<Response<List<User>>> GetAllAsync(Predicate<User> predicate)
    {
        var models = await this.genericRepository.GetAllAsync(predicate);
        return new Response<List<User>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = models
        };
    }

    public async Task<Response<User>> GetByIdAsync(Predicate<User> predicate)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
        {
            return new Response<User>()
            {
                StatusCode = 404,
                Message = "NotFound",
                Value = null
            };
        }
        return new Response<User>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    

    public async Task<Response<User>> UpdateAsync(Predicate<User> predicate, UserCreationDto user)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
        {
            return new Response<User>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Value = null
            };
        }
        var mappedUser = new User()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            Password = user.Password,
            Login = user.Login,
            UpdatedAt = DateTime.Now,

        };
        var result = await this.genericRepository.UpdateAsync(predicate, mappedUser);
        return new Response<User>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    

}
