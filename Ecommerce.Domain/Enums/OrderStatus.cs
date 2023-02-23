using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Enums
{
    public enum OrderStatus : byte
    {
        Pending = 10,
        Delivering = 20,
        Delivered = 30,
        Canceled = 40
    }
}
