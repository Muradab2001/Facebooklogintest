using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DAL;
using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult mesaj()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(ContactUs contactUs)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!ModelState.IsValid) return View();
                AppUser user = await _userManager.FindByNameAsync(contactUs.Name);
                if (user != null)
                {
                    ContactUs contact = new ContactUs
                    {
                        Name = contactUs.Name,
                        Email = contactUs.Email,
                        Subject = contactUs.Subject,
                        Message = contactUs.Message
                    };
                    await _context.ContactUs.AddAsync(contact);
                    await _context.SaveChangesAsync();
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Contact", "Erorr Msj");
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
