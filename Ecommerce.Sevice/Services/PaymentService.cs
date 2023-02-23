
using Ecommerce.Sevice.DTOs;
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.Interfaces;
using ECommerce.Domain.Entities;
using ECommmerce.Data.Repositories;

namespace Ecommerce.Sevice.Services;

public class PaymentService : IPaymentService
{

    private readonly GenericRepository<Payment> genericRepository = new GenericRepository<Payment>();
    private long lastId;
    public async Task<Response<Payment>> CreateAsync(PaymentCreationDto payment)
    {
        
        var models = await this.genericRepository.GetAllAsync(x => x.Id > 0);
        if (models.Count == 0)
            lastId = 1;
        else
            lastId = (models[models.Count - 1].Id) + 1;

        var mappedModel = new Payment()
        {
            Id= lastId,
            UserId=payment.UserId,
            Type = payment.Type,
            OrderId= payment.OrderId,
            IsPaid = true
        };
        var result = await this.genericRepository.CreateAsync(mappedModel);
        return new Response<Payment>()
        {
            StatusCode = 201,
            Message = "SUccessfully Created",
            Value = result
        };

    }

    public async Task<Response<bool>> DeleteAsync(Predicate<Payment> predicate)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if (model is null)
            return new Response<bool>()
            {
                StatusCode = 404,
                Message = "Payment is not found",
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

    public async Task<Response<List<Payment>>> GetAllAsync(Predicate<Payment> predicate)
    {
        var model = await this.genericRepository.GetAllAsync(predicate);
        return new Response<List<Payment>>()
        {
            StatusCode = 201,
            Message = "Success",
            Value = model
        };
    }

    public async Task<Response<Payment>> GetByIdAsync(Predicate<Payment> predicate)
    {
        var temp = await this.genericRepository.GetByIdAsync(predicate);
        if(temp is null)
        {
            return new Response<Payment>()
            {
                StatusCode = 401,
                Message = "User is not found",
                Value = null
            };
            
        }
        return new Response<Payment>()
        {
            StatusCode = 201,
            Message = "Success",
            Value = temp
        };
    }

    public async Task<Response<Payment>> UpdateAsync(Predicate<Payment> predicate, PaymentCreationDto payment)
    {
        var res = await this.genericRepository.GetByIdAsync(predicate);
        if (res is null)
            return new Response<Payment>()
            {
                StatusCode = 404,
                Message = "Payment is not found",
                Value = null
            };
        var mappedModel = new Payment()
        {
            Type = payment.Type
        };
        var result = await this.genericRepository.UpdateAsync(predicate, mappedModel);

        return new Response<Payment>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = result
        };
    }
}
