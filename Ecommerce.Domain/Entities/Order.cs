using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;

namespace ECommerce.Domain.Entities;


    public class Order : Auditable 
    {
        public List<Clothes> Products { get; set; }
        public string Adress { get; set; }
        public long CartId { get; set; }
        public Payment Payment { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public long UserId { get; set; }


    }
