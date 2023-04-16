using MultiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Models
{
    public class ClothesCategory:BaseEntity
    {
        public int ClothesId { get; set; }
        public Clothes Clothes { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
