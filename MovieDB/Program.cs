using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MovieDB.DAL;
using MovieDB.BLL;
using MovieDB.Models;

namespace MovieDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //register DbContext
            builder.Services.AddDbContext<MovieDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register MoviePlayListService and MoviePlayListDAL
            builder.Services.AddScoped<MoviePlayListService>();
            builder.Services.AddScoped<MoviePlayListDAL>();
            builder.Services.AddScoped<PlayListService>();
            builder.Services.AddScoped<PlayListDAL>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MoviePlayList}/{action=Create}/{id?}");

            app.Run();
        }
    }
}
