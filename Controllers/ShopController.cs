using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;
using MultiShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Shop(int? id)
        {
            List<Clothes> clothes = _context.Clothes.Include(c => c.ClothesInformation).Include(c => c.ClothesImages).Include(c => c.ClothesCategories).ToList();
            return View(clothes);
        }
    }
}

