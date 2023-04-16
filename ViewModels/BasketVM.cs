using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.ViewModels
{
    public class BasketVM
    {
        public List<BasketCookieItemVM> BasketCookieItemVModels { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
