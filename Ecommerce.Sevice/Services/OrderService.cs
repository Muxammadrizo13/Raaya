

using Ecommerce.Sevice.DTOs;
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommmerce.Data.Repositories;
using System.ComponentModel;

namespace Ecommerce.Sevice.Services;

public class OrderService : IOrderService
{
    ClothesService productService = new ClothesService();
    private readonly GenericRepository<Order> genericRepository = new GenericRepository<Order>();
    private readonly IPaymentService paymentService = new PaymentService();
    public async Task<Response<Order>> CreateAsync(OrderCreationDto order)
    {
        foreach(var item in order.Products)
        {
            var ourProducts = (await productService.GetByIdAsync(x => x.Id == item.Id)).Value;
            if(ourProducts is null)
            {
                return new Response<Order>()
                {
                    StatusCode = 201,
                    Message = "Some of the clothes doesn't exist",
                    Value = null
                };
            }
            if(ourProducts.Count < item.Count)
            {
                return new Response<Order>()
                {
                    StatusCode = 200,
                    Message = "Some of the clothes are not sufficient",
                    Value = null
                };
            }

        }
        foreach (var item in order.Products)
        {
            var OurProducts = (await productService.GetByIdAsync(x=>x.Id==item.Id)).Value;
            var mappedProduct = new ClothesCreationDto()
            {
                Count = OurProducts.Count - item.Count,
                Description = OurProducts.Describtion,
                Name = OurProducts.Name,
                Price = OurProducts.Price
            };
            await productService.UpdateAsync(x=>x.Id==OurProducts.Id, mappedProduct);
        }
        var PaymentRes = await paymentService.CreateAsync(order.Payment);
        if (PaymentRes.Value.IsPaid)
        {
            var mappedOrder = new Order()
            {
                Products = order.Products,
                Payment = PaymentRes.Value,
                Status = OrderStatus.Pending,

            };
            var OrderResult = await genericRepository.CreateAsync(mappedOrder);
            return new Response<Order>()
            {
                StatusCode = 200,
                Message = "Successfully ordered",
                Value = OrderResult
            };
        }
        return new Response<Order>
        {
            StatusCode = 201,
            Message = "Invalid Payment",
            Value = null
        };
    }

    public async Task<Response<bool>> DeleteAsync(Predicate<Order> predicate)
    {
        var temp = await this.genericRepository.GetByIdAsync(predicate);
        if(temp is null)
        {
            return new Response<bool>()
            {
                StatusCode = 201,
                Message = "Clothes is not found",
                Value = false
            };
        }
        await this.genericRepository.DeleteAysnc(predicate);
        return new Response<bool>()
        {
            StatusCode = 200,
            Message = "Successfully deleted",
            Value = true
        };

    }

    public async Task<Response<List<Order>>> GetAllAsync(Predicate<Order> predicate)
    {
        var temp = await this .genericRepository.GetAllAsync(predicate);
        return new Response<List<Order>>()
        {
            StatusCode = 200,
            Message = "Success",
            Value = temp
        };
    }

    public async Task<Response<Order>> GetByIdAsync(Predicate<Order> predicate)
    {
        var res = await this.genericRepository.GetByIdAsync(predicate);
        if(res is null)
        {
            return new Response<Order>()
            {
                StatusCode = 202,
                Message = "Order is not found",
                Value = null
            };
        }
        return new Response<Order>()
        {
            StatusCode = 200,
            Message = "Order found Successfully",
            Value = res
        };
    }

    public async Task<Response<Order>> UpdateAsync(Predicate<Order> predicate, OrderCreationDto order)
    {
        var model = await this.genericRepository.GetByIdAsync(predicate);
        if(model is null)
        {
            return new Response<Order>()
            {
                StatusCode = 202,
                Message = "Order is not found",
                Value = null
            };
        }
        var mappedPayment = new Payment()
        {
            OrderId = order.Payment.OrderId,
            Type = order.Payment.Type,
            IsPaid = order.Payment.IsPaid,
        };
        var mappedOrder = new Order()
        {
            Products = order.Products,
            Payment = mappedPayment,
            Status = model.Status
        };
        var OrderResult = await this.genericRepository.UpdateAsync(predicate, mappedOrder);
        return new Response<Order>()
        {
            StatusCode = 200,
            Message = "Order is updates successfully",
            Value = OrderResult
        };
    }
}
