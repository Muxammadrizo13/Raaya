using ECommerce.Domain.Enums;
using Raaya.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Sevice.DTOs
{
    public class ClothesCreationDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public ClothesType Type { get; set; }
        public ClothesBrand Brand { get; set; }
        public ClothesSize Size { get; set; }
    }
}
