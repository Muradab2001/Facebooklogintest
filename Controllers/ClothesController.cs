using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    public class ClothesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClothesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Detail(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Clothes clothes = _context.Clothes.Include(c => c.ClothesImages).Include(c => c.ClothesInformation).FirstOrDefault(c => c.Id == id);
            if (clothes == null) return NotFound();
            return View(clothes);
        }
    }
}
