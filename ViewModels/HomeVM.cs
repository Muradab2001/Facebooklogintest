using MultiShop.Models;
using System;
using System.Collections.Generic;

namespace MultiShop.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public List<Clothes> Clothes { get; set; }
    }
}
