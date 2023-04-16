using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;
using MultiShop.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<BasketItem> basket = await _context.BasketItems.Include(o => o.AppUser)
                .Include(o => o.Clothes)
                .Where(o => o.AppUserId == user.Id).ToListAsync();
            order.Date = DateTime.Now;
            order.Price = default;
            order.TotalPrice = default;
            order.AppUser = user;
            order.BasketItems = basket;
            foreach (BasketItem item in basket)
            {
                order.TotalPrice += item.Price * item.Quantity;
            }
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
