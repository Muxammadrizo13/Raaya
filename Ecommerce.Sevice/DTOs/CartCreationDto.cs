
using ECommerce.Domain.Entities;

namespace Ecommerce.Sevice.DTOs;

public class CartCreationDto
{
    public List<Clothes> Products { get; set; }
}
