
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Sevice.DTOs
{
    public class PaymentCreationDto
    {
        public PaymentType Type { get; set; }
        public long OrderId { get; set; }
        public bool IsPaid { get; set; } 
        public long UserId { get; set; }
    }
}
