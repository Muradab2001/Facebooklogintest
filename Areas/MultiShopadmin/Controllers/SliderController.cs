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
    public class SliderController : Controller
    {
        private ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public SliderController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> model = _context.Sliders.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid) return View();
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "You must select 1 photo");
                return View();
            }
            if (!slider.Photo.ImageIsOkay(2))
            {
                ModelState.AddModelError("Photo", "Please choose valid image file");
                return View();
            }
            slider.Image = await slider.Photo.FileCreate(_env.WebRootPath, "assets/img");

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id == 0) return NotFound();

            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider is null) return NotFound();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();
            FileValidator.FileDelete(_env.WebRootPath, "assets/img", slider.Image);
            return View(slider);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id,Slider slider)
        {
            if (id == null || id == 0) return NotFound();
            Slider existed = await _context.Sliders.FindAsync(id);
            if (existed == null) return NotFound();
            if (!ModelState.IsValid) return View(slider);
            if (slider.Photo == null)
            {
                string filename = existed.Image;
                _context.Entry(existed).CurrentValues.SetValues(slider);
                existed.Image = filename;
            }
            else
            {
                if (!slider.Photo.ImageIsOkay(2))
                {
                    ModelState.AddModelError("Photo", "choose image file big");
                    return View(existed);
                }
                FileValidator.FileDelete(_env.WebRootPath, "assets/img", existed.Image);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
