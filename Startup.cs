using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiShop.DAL;
using MultiShop.Hubs;
using MultiShop.Models;
using MultiShop.Services;
using Microsoft.AspNetCore.Authentication.Facebook;
using System;
using System.Threading.Tasks;

namespace MultiShop
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
      .AddFacebook(options =>
      {
          options.AppId = "1673915823034899";
          options.AppSecret = "dcfdc6a0d65e626186d71c0771c94f6d";
          options.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
          {
              OnRemoteFailure = ctx =>
              {
                  ctx.Response.Redirect("/Error");
                  ctx.HandleResponse();
                  return Task.CompletedTask;
              }
          };
          options.Scope.Add("email");
      });
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(_config.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789";
                opt.Password.RequiredUniqueChars = 3;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(100);
                opt.Lockout.AllowedForNewUsers = true;

            }).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<LayoutService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute("defualt", "{controller=home}/{action=index}/{id?}");
                endpoints.MapHub<ChatHub>("/myhub");
            });
        }
    }
}
