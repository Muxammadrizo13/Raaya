using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Payment : Auditable 
    {
        public PaymentType Type { get; set; }
        public bool IsPaid { get; set; }
        public long UserId { get; set; }
        public Order Order { get; set; }

        public long OrderId { get; set; }
    }
}
