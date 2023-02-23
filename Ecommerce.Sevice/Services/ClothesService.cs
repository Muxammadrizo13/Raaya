
using Ecommerce.Sevice.DTOs;
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.Interfaces;
using ECommerce.Domain.Entities;
using ECommmerce.Data.Repositories;

namespace Ecommerce.Sevice.Services;

public class ClothesService : IClothesService
{
    private readonly GenericRepository<Clothes> genericRepository = new GenericRepository<Clothes>();
    private long lastId;
    public async Task<Response<Clothes>> CreateAsync(ClothesCreationDto product)
    {
        var models = await this.genericRepository.GetAllAsync(x => x.Id > 0);
        if (models.Count == 0)
            lastId = 1;
        else
            lastId = (models[models.Count - 1].Id) + 1;

        var model = models.FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());
        if (model is not null)
        {
            model.Count += product.Count;
            await genericRepository.UpdateAsync(x => x.Id == model.Id, model);

            return new Response<Clothes>()
            {
                StatusCode = 403,
                Message = "Already exists",
                Value = null
            };

        }

        var mappedProduct = new Clothes()
        {
            Id = lastId,
            Name = product.Name,
            Describtion = product.Description,
            Price = product.Price,
            Count = product.Count,
            CreatedAt = DateTime.Now,
            Type = product.Type,
            Brand = product.Brand
        };

        var result = await this.genericRepository.CreateAsync(mappedProduct);
        return new Response<Clothes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }

    public async Task<Response<bool>> DeleteAsync(Predicate<Clothes> predicate)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
        {
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Value = false
            };
        }
        var result = await this.genericRepository.DeleteAysnc(predicate);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = true
        };
    }

    public async Task<Response<List<Clothes>>> GetAllAsync(Predicate<Clothes> predicate)
    {
        var models = await this.genericRepository.GetAllAsync(predicate);
        return new Response<List<Clothes>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = models
        };
    }

    public async Task<Response<Clothes>> GetByIdAsync(Predicate<Clothes> predicate)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
        {
            return new Response<Clothes>()
            {
                StatusCode = 404,
                Message = "NotFound",
                Value = null
            };
        }
        return new Response<Clothes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Clothes>> UpdateAsync(Predicate<Clothes> predicate, ClothesCreationDto product)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
        {
            return new Response<Clothes>()
            {
                StatusCode = 404,
                Message = "NOT FOUND",
                Value = null
            };
        }
        var mappedProduct = new Clothes()
        {
            Name = product.Name,
            Describtion = product.Description,
            Count = product.Count,
            Price = product.Price,
            UpdatedAt = DateTime.Now,
            Type = product.Type,
            Brand = product.Brand

        };
        var result = await this.genericRepository.UpdateAsync(predicate, mappedProduct);
        return new Response<Clothes>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
