using ECommerce.Domain.Commons;
using ECommerce.Domain.Enums;
using Raaya.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Entities
{
    public class Clothes : Auditable 
    {
        public string Name { get; set; }
        public string Describtion { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public ClothesSize Size { get; set; }
        public ClothesBrand Brand { get; set; }
        public ClothesType Type { get; set; }


    }
}
