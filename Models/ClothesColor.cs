using MultiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Models
{
    public class ClothesColor:BaseEntity
    {
        public string Color { get; set; }
        public int ClothesId { get; set; }
        public Clothes Clothes { get; set; }
    }
}
