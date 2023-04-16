using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiShop.DAL
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }


        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<ClothesCategory> ClothesCategories { get; set; }
        public DbSet<ClothesInformation> clothesInformations { get; set; }
        public DbSet<ClothesImage> ClothesImages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Product> Products { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var item in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                )
            {
                item.SetColumnType("decimal(6,2)");
            }

            modelBuilder.Entity<Setting>()
                .HasIndex(p => p.Key)
                .IsUnique();

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
