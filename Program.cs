using JopShop.Models;
using Microsoft.EntityFrameworkCore;

namespace JopShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();





            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });







            // Register the DbContext with the DI container 
            builder.Services.AddDbContext<JopShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString
            ("DefaultConnection")));

builder.Services.AddHttpContextAccessor(); // ✅ لحل الخطأ

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();






            app.UseSession();








            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
