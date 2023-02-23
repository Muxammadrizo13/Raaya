

using ECommerce.Domain.Entities;

namespace Ecommerce.Sevice.DTOs
{
    public class OrderCreationDto
    {
        public string Adress { get; set; }
        public PaymentCreationDto Payment { get; set; }
        public List<Clothes> Products { get; set; }
       
    }
}
