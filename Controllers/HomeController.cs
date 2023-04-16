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
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Sliders = _context.Sliders.ToList(),
                Categories = _context.Categories.Include(x=>x.ClothesCategory).ToList(),
                Clothes=_context.Clothes.Include(x=>x.ClothesImages).Include(x=>x.ClothesCategories).ThenInclude(x=>x.Category).Include(x=>x.ClothesInformation).ToList()
            };
            return View(model);

        }
    }
}
