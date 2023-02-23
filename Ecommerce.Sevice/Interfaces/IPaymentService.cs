
using ECommerce.Domain.Entities;
using Ecommerce.Sevice.Helpers;
using Ecommerce.Sevice.DTOs;

namespace Ecommerce.Sevice.Interfaces;

public interface IPaymentService
{
    Task<Response<Payment>> CreateAsync(PaymentCreationDto payment);
    Task<Response<Payment>> UpdateAsync(Predicate<Payment> predicate, PaymentCreationDto payment);
    Task<Response<bool>> DeleteAsync(Predicate<Payment> predicate);
    Task<Response<Payment>> GetByIdAsync(Predicate<Payment> predicate);
    Task<Response<List<Payment>>> GetAllAsync(Predicate<Payment> predicate = null);
    //good
}
