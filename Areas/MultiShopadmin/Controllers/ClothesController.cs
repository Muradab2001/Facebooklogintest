using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

    public class ClothesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ClothesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Clothes> clothes = await _context.Clothes.Include(p => p.ClothesImages)
                .Include(p => p.ClothesCategories)
                .ThenInclude(pc => pc.Category)
                .ToListAsync();

            return View(clothes);
        }
        public IActionResult Create()
        {
            ViewBag.Information = _context.clothesInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Clothes clothes)
        {
            ViewBag.Information = _context.clothesInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (clothes.MainPhoto == null || clothes.Photos == null)
            {

                ModelState.AddModelError(string.Empty, "must choose 1 main photo");
                return View();
            }
            if (!clothes.MainPhoto.ImageIsOkay(2))
            {

                ModelState.AddModelError(string.Empty, "choose image file");
                return View();
            }

            clothes.Image = await FileValidator.FileCreate(clothes.MainPhoto, _env.WebRootPath, "assets/img");

            ClothesImage mainimage = new ClothesImage
            {
                IsMain = true,
                Name = await FileValidator.FileCreate(clothes.MainPhoto, _env.WebRootPath, "assets/img")
            };
           

            clothes.ClothesImages = new List<ClothesImage>();

            clothes.ClothesImages.Add(mainimage);

            clothes.ClothesCategories = new List<ClothesCategory>();

            if (clothes.Photos.Count < 0)
            {
                ModelState.AddModelError("", "choose image file");
                return View();
            }

            foreach (IFormFile file in clothes.Photos)
            {
                if (!file.ImageIsOkay(2))
                {
                    ModelState.AddModelError(string.Empty, "choose valid image file");
                    return View();
                }

                ClothesImage clothesImage = new ClothesImage
                {
                    IsMain = false,
                    Clothes = clothes,
                    Name = await FileValidator.FileCreate(file, _env.WebRootPath, "assets/img")
                };
                clothes.ClothesImages.Add(clothesImage);
            }

            foreach (int id in clothes.CategoryIds)
            {
                ClothesCategory clothesCategory = new ClothesCategory
                {
                    CategoryId = id,
                    Clothes = clothes
                };

                clothes.ClothesCategories.Add(clothesCategory);
            };

            _context.Clothes.Add(clothes);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Clothes clothes = _context.Clothes.SingleOrDefault(c => c.Id == id);
            if (clothes == null) return NotFound();
            return View(clothes);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Information = _context.clothesInformations.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.ClothesCategories=_context.ClothesCategories.Where(c=>c.ClothesId==id).ToList();
            if (id == 0 || id == null) return NotFound();
            if (!ModelState.IsValid) return View();
            Clothes clothes = await _context.Clothes
                .Include(c => c.ClothesImages)
                .Include(c => c.ClothesInformation)
                .Include(c => c.ClothesCategories).SingleOrDefaultAsync(c => c.Id == id);
            if (clothes == null) return NotFound();
            return View(clothes);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, Clothes clothes)
        {
            if (id == 0 || id == null) return NotFound();
            Clothes existed = await _context.Clothes
               .Include(c => c.ClothesImages)
               .Include(c => c.ClothesInformation)
               .Include(c => c.ClothesCategories).FirstOrDefaultAsync(c => c.Id == id);
            if (!ModelState.IsValid) return View(existed);
            if (existed == null) return NotFound();
            List<ClothesImage> clothesImages = existed.ClothesImages.Where(c => c.IsMain == true &&
            !clothes.ImageId.Contains(c.Id)).ToList();

            _context.Entry(existed).CurrentValues.SetValues(clothes);
            existed.ClothesImages.RemoveAll(c => clothesImages.Any(r => c.Id == r.Id));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            Clothes clothes = await _context.Clothes.FindAsync(id);
            if (clothes == null) return NotFound();
            _context.Clothes.Remove(clothes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
