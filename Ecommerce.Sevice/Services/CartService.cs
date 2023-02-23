
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.Interfaces;
using ECommerce.Domain.Entities;
using ECommmerce.Data.Repositories;


namespace Ecommerce.Sevice.Services;

public class CartService : ICartService
{
    private readonly GenericRepository<Cart> cardRepo = new GenericRepository<Cart>();

    public async Task<Response<Cart>> AddProductAsync(long userId, Clothes product)
    {
        var models = await this.cardRepo.GetAllAsync(x => x.Id > 0);
        var model = models.FirstOrDefault(x => x.UserId == userId);
        if (model is null)
        {
            return new Response<Cart>()
            {
                StatusCode = 404,
                Message = "Not Found",
                Value = null
            };
        }
        var temp = model.Products;
        temp.Add(product);
        
        var mappedCart = new Cart()
        {
            Id= userId,
            UserId= userId,
            Products = temp
        };

        var result = await this.cardRepo.UpdateAsync(x => x.UserId == userId, mappedCart);

        return new Response<Cart>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<bool>> ClearAsync(long userId)
    {
        var model = await cardRepo.GetByIdAsync(x => x.UserId == userId);
        if (model is null)
        {
            return new Response<bool>
            {
                StatusCode = 403,
                Message = "Not Found",
                Value = false
            };
        }
        var mappaedCart = new Cart()
        {
            Id = model.Id,
            UserId = userId,
    
        };
        var result = await cardRepo.UpdateAsync(x=>x.UserId==userId, mappaedCart);
        return new Response<bool>
        {
            StatusCode = 200,
            Message = "Not Found",
            Value = true
        };
    }

    public async Task<Response<Cart>> CreateAsync(long userId)
    {
        var newCart = new Cart()
        {
            Id = userId,
            UserId = userId,
            Products= new List<Clothes>()
        };

        var cart = await cardRepo.CreateAsync(newCart);

        return new Response<Cart>()
        {
            StatusCode = 200,
            Message="Succsess",
            Value=cart
        };
    }

    public async Task<Response<Cart>> GetCartAsync(Predicate<Cart> predicate)
    {
        var model = await this.cardRepo.GetByIdAsync(predicate);
        if (model is null )
        {
            return new Response<Cart>()
            {
                StatusCode = 404,
                Message = "Not Found",
                Value = null
            };
        }
        return new Response<Cart>()
        {
            StatusCode = 200,
            Message = "Succsess",
            Value = model
        };
    }

    public async Task<Response<Cart>> UpdateAsync(long userId, Cart cart)
    {
        var models = await this.cardRepo.GetAllAsync(x => x.Id > 0);
        var model = models.FirstOrDefault(x => x.UserId == userId);
        if (model is null)
        {
            return new Response<Cart>()
            {
                StatusCode = 404,
                Message = "Not Found",
                Value = null
            };
        }
        var mappedCart = new Cart()
        {
            Id= userId,
            UserId= userId,
            Products = cart.Products,
        };

        var result = await this.cardRepo.UpdateAsync(x => x.UserId == userId, mappedCart);

        return new Response<Cart>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
