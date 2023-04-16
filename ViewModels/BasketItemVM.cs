using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.ViewModels
{
    public class BasketItemVM
    {
        public Clothes Clothes { get; set; }
        public int Quantity { get; set; }
    }
}
