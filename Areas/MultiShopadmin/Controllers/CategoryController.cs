using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.DAL;
using MultiShop.Models;
using MultiShop.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Areas.MultiShopadmin.Controllers
{
    [Area("MultiShopadmin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public CategoryController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Category> model = _context.Categories.OrderByDescending(x => x.Id).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View();
            if (category.Photo == null)
            {
                ModelState.AddModelError("Photo", "You must select 1 photo");
                return View();
            }
            if (!category.Photo.ImageIsOkay(2))
            {
                ModelState.AddModelError("Photo", "Please choose valid image file");
                return View();
            }
            category.Image = await category.Photo.FileCreate(_env.WebRootPath, "assets/img");

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Category category = await _context.Categories.FindAsync(id);
            if (category is null) return NotFound();
            FileValidator.FileDelete(_env.WebRootPath, "assets/img", category.Image);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == id);
            if (category is null) return NotFound();
            return View(category);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, Category category)
        {
            Category existed = await _context.Categories.FindAsync(id);
            if (existed is null) return NotFound();
            if (!ModelState.IsValid) return View(existed);
            bool duplicate = _context.Categories.Any(c => c.Name == category.Name);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "You cannot duplicate name");
                return View();
            }
      
            if (category.Photo == null)
            {
                string filename = existed.Image;
                _context.Entry(existed).CurrentValues.SetValues(category);
                existed.Image = filename;
            }
            else
            {
                if (!category.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Photo", "Please choose valid image file");
                    return View(existed);
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/img", existed.Image);
                _context.Entry(existed).CurrentValues.SetValues(category);
                existed.Image = await category.Photo.FileCreate(_env.WebRootPath, "assets/img");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
