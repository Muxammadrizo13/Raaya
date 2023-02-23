using ECommerce.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Cart : Auditable
    {
        public List<Clothes> Products { get; set; }
        public Cart()
        {
            Products= new List<Clothes>();
        }
        public long UserId { get; set; }

    }
}
