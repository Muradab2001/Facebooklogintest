using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;
using MultiShop.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Services
{
    public class LayoutService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _http;

        public LayoutService(ApplicationDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }
        public List<Setting> GetSettings()
        {
            List<Setting> settings = _context.Settings.ToList();
            return settings;
        }
        public List<Category> GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            return categories;
        }
        public LayoutBasketVM GetBasket()
        {
            string basketStr = _http.HttpContext.Request.Cookies["Basket"];
            if (!string.IsNullOrEmpty(basketStr))
            {
                BasketVM basket = JsonConvert.DeserializeObject<BasketVM>(basketStr);
                LayoutBasketVM layoutBasket = new LayoutBasketVM();
                layoutBasket.BasketItemVMs = new List<BasketItemVM>();
                foreach (BasketCookieItemVM cookie in basket.BasketCookieItemVModels)
                {
                    Clothes existed = _context.Clothes.Include(c => c.ClothesImages)
                        .FirstOrDefault(c => c.Id == cookie.Id);
                    if (existed == null)
                    {
                        basket.BasketCookieItemVModels.Remove(cookie);
                        continue;
                    }
                    BasketItemVM basketItem = new BasketItemVM
                    {
                        Clothes = existed,
                        Quantity = cookie.Quantity
                    };
                    layoutBasket.BasketItemVMs.Add(basketItem);
                }
                layoutBasket.TotalPrice = basket.TotalPrice;
                return layoutBasket;
            }
            return null;
        }
    }
}
