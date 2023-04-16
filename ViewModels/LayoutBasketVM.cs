using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.ViewModels
{
    public class LayoutBasketVM
    {
        public List<BasketCookieItemVM> BasketCookieItemVMs { get; set; }
        public decimal TotalPrice { get; set; }
        public List<BasketItemVM> BasketItemVMs{ get; set; }
    }
}
