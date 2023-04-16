using MultiShop.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Models
{
    public class Order:BaseEntity
    {
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


    }
}
